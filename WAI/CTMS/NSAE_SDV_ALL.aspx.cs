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
    public partial class NSAE_SDV_ALL : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_SAE dal_SAE = new DAL_SAE();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    FillINV();
                    FillSubject();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            DataSet ds = dal.GET_INVID_SP(
                    USERID: Session["User_ID"].ToString()
                    );
            drpInvID.DataSource = ds.Tables[0];
            drpInvID.DataValueField = "INVID";
            drpInvID.DataBind();

            FillSubject();
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAE_SUBJECTS",
                    INVID: drpInvID.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "0"));
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
                DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAEID_LIST",
                    SUBJID: drpSubID.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSAEID.DataSource = ds.Tables[0];
                    drpSAEID.DataValueField = "SAEID";
                    drpSAEID.DataTextField = "SAEID";
                    drpSAEID.DataBind();
                    drpSAEID.Items.Insert(0, new ListItem("--Select--", "0"));
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

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                GET_MODULE();
                GET_SAEID();
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
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_MODULE_SP(ACTION: "GET_SAE_MODULE");

                //if (Convert.ToString(Session["UserType"]).Contains("Site"))
                //{
                //    ds = dal_SAE.SAE_MODULE_SP(ACTION: "GET_SAE_MODULE");
                //}
                //else
                //{
                //    ds = dal_SAE.SAE_MODULE_SP(ACTION: "GET_SAE_MODULE_MM");
                //}

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
                    Control anchor = e.Item.FindControl("anchor") as Control;

                    GridView grd_Records = (GridView)e.Item.FindControl("grd_Records");

                    DataSet ds = dal_SAE.SAE_SDVDETAILS_SP(
                            ACTION: "GET_SAE_RECORD",
                            SAEID: drpSAEID.SelectedValue,
                            TABLENAME: row["TABLENAME"].ToString(),
                            MODULEID: row["MODULEID"].ToString()
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

                LinkButton lnkQUERYSTATUS = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                HtmlControl ADICON = (HtmlControl)e.Row.FindControl("ADICON");
                LinkButton lbtnSDV = (LinkButton)e.Row.FindControl("lbtnSDV");
                LinkButton lbtnSDVDONE = (LinkButton)e.Row.FindControl("lbtnSDVDONE");

                if (dr["SDVSTATUS"].ToString() == "1" || dr["SDVSTATUS"].ToString() == "2")
                {
                    lbtnSDVDONE.CssClass = lbtnSDVDONE.CssClass.Replace("disp-none", "");
                    lbtnSDV.CssClass = "disp-none";
                }
                else
                {
                    lbtnSDVDONE.CssClass = "disp-none";
                    lbtnSDV.CssClass = lbtnSDV.CssClass.Replace("disp-none", "");
                }

                string QryCount = dr["QryCount"].ToString();

                if (QryCount == "0")
                {
                    lnkQUERYSTATUS.Attributes.Add("class", "disp-none");
                }
                LinkButton lnkQueryAns = (LinkButton)e.Row.FindControl("lnkQUERYANS");
                if (dr["QryAnsCount"].ToString() != "0")
                {
                    lnkQueryAns.Visible = true;
                }
                else
                {
                    lnkQueryAns.Visible = false;
                }
                LinkButton lnkQueryClose = (LinkButton)e.Row.FindControl("lnkQUERYCLOSE");
                if (dr["QryClosedCount"].ToString() != "0")
                {
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

                string DELETE = dr["DELETE"].ToString();

                if (DELETE == "True")
                {
                    e.Row.Attributes.Add("class", "brd-1px-maroonimp");
                    lbtnSDV.Visible = false;
                    lbtnSDVDONE.Visible = false;
                }

                GridView grd_Records = (GridView)sender;

                grd_Records.HeaderRow.Cells[10].Visible = false;
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

                e.Row.Cells[10].Visible = false;
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