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
    public partial class NCTMS_VISITS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    FillINV();
                    //FillVISITS();

                    GETDATA();
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
                ddlSiteId.Items.Insert(0, new ListItem("--Select Site--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillVISITS()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_VISITTYPE_MASTER"
                );

                if (ddlSiteId.SelectedValue != "0")
                {
                    ddlVisitType.DataSource = ds.Tables[0];
                    ddlVisitType.DataValueField = "ID";
                    ddlVisitType.DataTextField = "VISIT_NAME";
                    ddlVisitType.DataBind();
                    ddlVisitType.Items.Insert(0, new ListItem("--Select Visit--", "0"));
                }
                else
                {
                    ddlVisitType.Items.Clear();
                    txtVisitNomenclature.Text = "";
                    txtStartDate.Text = "";
                    txtEndDate.Text = "";
                    ddlEmployee.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FILL_EMPLOYEE()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_EMPLOYEES"
                );

                ddlEmployee.DataSource = ds.Tables[0];
                ddlEmployee.DataValueField = "User_ID";
                ddlEmployee.DataTextField = "User_Name";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("--Select User--", "0"));
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
                FillVISITS();
                FILL_EMPLOYEE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_VISIT_NOM",
                VISITID: ddlVisitType.SelectedValue,
                SITEID: ddlSiteId.SelectedValue
                );

                txtVisitNomenclature.Text = ds.Tables[0].Rows[0]["VISIT_NOM"].ToString();
            }
            catch (Exception ex)
            {

            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                
                    ACTION: "GET_SITE_VISIST",
                VISITID: ddlVisitType.SelectedValue,
                SITEID: ddlSiteId.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdVisits.DataSource = ds;
                    grdVisits.DataBind();
                }
                else
                {
                    grdVisits.DataSource = null;
                    grdVisits.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "INSERT_SITE_VISITS",
                VISITID: ddlVisitType.SelectedValue,
                SITEID: ddlSiteId.SelectedValue,
                VISIT_NOM: txtVisitNomenclature.Text,
                STARTDAT: txtStartDate.Text,
                ENDDAT: txtEndDate.Text,
                EMPID: ddlEmployee.SelectedValue
                );

                if (ds.Tables.Count > 0)
                {
                    Response.Write("<script> alert('" + ds.Tables[0].Rows[0]["MSG"].ToString() + "');window.location='CTMS_VISITS.aspx'; </script>");
                }
                else
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdVisits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;

                if (e.CommandName == "EDITVisit")
                {
                    EDIT_SITE_VISIT(ID);
                }
                else if (e.CommandName == "DeleteVisit")
                {
                    DELETE_SITE_VISIT(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void EDIT_SITE_VISIT(string ID)
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_SITE_VISIST_BYID", ID: ID);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    FillVISITS();
                    ddlVisitType.SelectedValue = ds.Tables[0].Rows[0]["VISITID"].ToString();

                    FillINV();
                    ddlSiteId.SelectedValue = ds.Tables[0].Rows[0]["SITEID"].ToString();

                    txtVisitNomenclature.Text = ds.Tables[0].Rows[0]["VISIT_NOM"].ToString();

                    FILL_EMPLOYEE();
                    ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["EMPID"].ToString();

                    txtStartDate.Text = ds.Tables[0].Rows[0]["STARTDT"].ToString();

                    txtEndDate.Text = ds.Tables[0].Rows[0]["ENDDT"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETE_SITE_VISIT(string ID)
        {
            try
            {
                dal.CTMS_DATA_SP(ACTION: "DELETE_SITE_VISIST", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvEmp_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void grdVisits_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    Label lblVISIT_NOM = (Label)e.Row.FindControl("lblVISIT_NOM");
                    LinkButton lbtnVISIT_NOM = (LinkButton)e.Row.FindControl("lbtnVISIT_NOM");

                    if (dr["SUB_STATUS"].ToString() == "Not Started / In-Progress" || dr["SUB_STATUS"].ToString() == "Return To CRA")
                    {
                        lbtnVISIT_NOM.CssClass = lbtnVISIT_NOM.CssClass.Replace("disp-none", "");
                        lblVISIT_NOM.CssClass = "disp-none";
                    }
                    else
                    {
                        lblVISIT_NOM.CssClass = lblVISIT_NOM.CssClass.Replace("disp-none", "");
                        lbtnVISIT_NOM.CssClass = "disp-none";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}