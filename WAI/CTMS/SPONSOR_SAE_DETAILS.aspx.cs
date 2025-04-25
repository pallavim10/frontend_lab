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
    public partial class SPONSOR_SAE_DETAILS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["UserGroup_ID"] == null)
                    {
                        Response.Redirect("SessionExpired.aspx", true);
                        return;
                    }

                    FillINV();
                    FillSubject();

                    if (Request.QueryString["SAEID"] != null)
                    {
                        GET_SAEID();
                        GET_SAE_STATUS();
                        GET_MODULE();
                        btngetdata.Visible = false;
                        drpInvID.Enabled = false;
                        drpSubID.Enabled = false;
                        drpSAEID.Enabled = false;
                        ddlStatus.Enabled = false;
                    }
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

                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataValueField = "INVNAME";
                drpInvID.DataBind();

                if (Request.QueryString["INVID"] != null)
                {
                    if (drpInvID.Items.Contains(new ListItem(Request.QueryString["INVID"].ToString())))
                    {
                        drpInvID.SelectedValue = Request.QueryString["INVID"].ToString();
                    }
                }

                FillSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE(ACTION: "GET_SAE_SUBJECTS", INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "0"));

                    if (Request.QueryString["SUBJID"] != null)
                    {
                        if (drpSubID.Items.Contains(new ListItem(Request.QueryString["SUBJID"].ToString())))
                        {
                            drpSubID.SelectedValue = Request.QueryString["SUBJID"].ToString();
                        }
                    }
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

        private void GET_SAEID()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE_NEW(ACTION: "GET_SAEIDS_SPONSOR", SUBJECTID: drpSubID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSAEID.DataSource = ds.Tables[0];
                    drpSAEID.DataValueField = "SAEID";
                    drpSAEID.DataTextField = "SAEID";
                    drpSAEID.DataBind();
                    drpSAEID.Items.Insert(0, new ListItem("--Select--", "0"));

                    if (Request.QueryString["SAEID"] != null)
                    {
                        if (drpSAEID.Items.Contains(new ListItem(Request.QueryString["SAEID"].ToString())))
                        {
                            drpSAEID.SelectedValue = Request.QueryString["SAEID"].ToString();
                        }
                    }
                }
                else
                {
                    drpSAEID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GET_SAE_STATUS()
        {
            try
            {
                DataSet ds = new DataSet();

                if (Convert.ToString(Request.QueryString["STATUS"]) != "Initial; Incomplete")
                {
                    ds = dal.SAE_ADD_UPDATE_NEW(ACTION: "GET_SAE_STATUS_SPONSOR",
                    SAEID: drpSAEID.SelectedValue
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlStatus.DataSource = ds;
                        ddlStatus.DataTextField = "STATUS";
                        ddlStatus.DataBind();

                        if (Request.QueryString["STATUS"] != null)
                        {
                            if (ddlStatus.Items.Contains(new ListItem(Request.QueryString["STATUS"].ToString())))
                            {
                                ddlStatus.SelectedValue = Request.QueryString["STATUS"].ToString();
                            }
                        }
                    }
                    else
                    {
                        ddlStatus.Items.Clear();
                    }
                }
                else
                {
                    ddlStatus.Items.Insert(0, new ListItem("Initial; Incomplete", "Initial; Incomplete"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                GET_SAEID();
                GET_SAE_STATUS();
                GET_MODULE();
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
                GET_SAEID();
                GET_SAE_STATUS();
                GET_MODULE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSAEID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SAE_STATUS();
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
                GET_MODULE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_MODULE()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE(ACTION: "GET_SAE_MODULES",
                   SUBJECTID: drpSubID.SelectedValue,
                   INVID: drpInvID.SelectedValue,
                   SAEID: drpSAEID.SelectedValue
                   );

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
                    Label lblINVSIGNOFF = (Label)e.Item.FindControl("lblINVSIGNOFF");
                    Label lblMR = (Label)e.Item.FindControl("lblMR");
                    Control anchor = e.Item.FindControl("anchor") as Control;
                    Control divSignoff = e.Item.FindControl("divSignoff") as Control;
                    Control divMR = e.Item.FindControl("divMR") as Control;

                    if (row["SIGNATURE"].ToString() == "1")
                    {
                        lblINVSIGNOFF.Text = row["SIGNATUREBY"].ToString() + " ( " + row["SIGNATURE_DT"].ToString() + " )";
                    }
                    else
                    {
                        lblINVSIGNOFF.Text = "Not Signed";
                    }

                    if (row["MR_STATUS"].ToString() == "1")
                    {
                        lblMR.Text = row["MRBY"].ToString() + " ( " + row["MRDAT"].ToString() + " )";
                    }
                    else
                    {
                        lblMR.Text = "Not Reviewed";
                    }

                    GridView grd_Records = (GridView)e.Item.FindControl("grd_Records");


                    DataSet ds = dal.SAE_ADD_UPDATE_NEW(
                                  ACTION: "GET_MODULE_DATA_SPONSOR",
                                  SAEID: drpSAEID.SelectedValue,
                                  INVID: drpInvID.SelectedValue,
                                  MODULEID: row["MODULEID"].ToString(),
                                  SUBJECTID: drpSubID.SelectedValue,
                                  STATUS: ddlStatus.SelectedValue
                                  );

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            if (dc.DataType.Name == "String")
                            {
                                ds.Tables[0].Rows[i][dc.ColumnName] = REMOVEHTML(ds.Tables[0].Rows[i][dc.ColumnName].ToString());
                            }
                        }
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_Records.DataSource = ds;
                        grd_Records.DataBind();

                        divSignoff.Visible = true;
                        anchor.Visible = true;
                        divMR.Visible = true;
                    }
                    else
                    {
                        grd_Records.DataSource = null;
                        grd_Records.DataBind();

                        divSignoff.Visible = false;
                        anchor.Visible = false;
                        divMR.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");
            str = str.Replace("&nbsp;", "");

            return str;
        }

        protected void grd_Records_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lnkPAGENUM = (LinkButton)e.Row.FindControl("lnkPAGENUM");
                Label lblRECID = (Label)e.Row.FindControl("lblRECID");
                HtmlImage lnkMANUALQUERY = (HtmlImage)e.Row.FindControl("lnkMANUALQUERY");
                HtmlImage lnkAUDITTRAIL = (HtmlImage)e.Row.FindControl("lnkAUDITTRAIL");
                HtmlImage ADINITIAL = (HtmlImage)e.Row.FindControl("ADINITIAL");
                HtmlImage NOTES = (HtmlImage)e.Row.FindControl("NOTES");
                //LinkButton lbtnSDV = (LinkButton)e.Row.FindControl("lbtnSDV");
                //LinkButton lbtnSDVDONE = (LinkButton)e.Row.FindControl("lbtnSDVDONE");

                GridView grd_Records = (GridView)sender;

                //if (dr["SDVSTATUS"].ToString() == "1")
                //{
                //    lbtnSDVDONE.CssClass = lbtnSDVDONE.CssClass.Replace("disp-none", "");
                //    lbtnSDV.CssClass = "disp-none";
                //}
                //else
                //{
                //    lbtnSDVDONE.CssClass = "disp-none";
                //    lbtnSDV.CssClass = lbtnSDV.CssClass.Replace("disp-none", "");
                //}

                if (dr["AUDITSTATUS"].ToString() == "")
                {
                    lnkAUDITTRAIL.Attributes.Add("class", "disp-none");
                    ADINITIAL.Attributes.Add("class", "disp-none");
                }
                else if (dr["AUDITSTATUS"].ToString() != "Initial Entry")
                {
                    ADINITIAL.Attributes.Add("class", "disp-none");
                    lnkAUDITTRAIL.Attributes.Add("class", "");
                }
                else
                {
                    lnkAUDITTRAIL.Attributes.Add("class", "disp-none");
                    ADINITIAL.Attributes.Add("class", "");
                }

                string DELETE = dr["DELETE"].ToString();

                if (DELETE == "True")
                {
                    e.Row.Attributes.Add("class", "strikeThrough");
                    lnkPAGENUM.Visible = false;
                    ADINITIAL.Attributes.Add("class", "disp-none");
                    lnkAUDITTRAIL.Attributes.Add("class", "disp-none");
                }
                else
                {
                    lnkPAGENUM.Visible = true;

                    lnkAUDITTRAIL.Visible = true;
                }

                if (dr["QUERYCOUNT"].ToString() != "0")
                {
                    lnkMANUALQUERY.Attributes.Add("class", "");
                }
                else
                {
                    lnkMANUALQUERY.Attributes.Add("class", "disp-none");
                }

                grd_Records.HeaderRow.Cells[11].Visible = false;
                grd_Records.HeaderRow.Cells[12].Visible = false;
                grd_Records.HeaderRow.Cells[13].Visible = false;
                grd_Records.HeaderRow.Cells[14].Visible = false;
                grd_Records.HeaderRow.Cells[15].Visible = false;
                grd_Records.HeaderRow.Cells[16].Visible = false;
                grd_Records.HeaderRow.Cells[17].Visible = false;
                grd_Records.HeaderRow.Cells[18].Visible = false;
                grd_Records.HeaderRow.Cells[19].Visible = false;
                grd_Records.HeaderRow.Cells[20].Visible = false;

                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;
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