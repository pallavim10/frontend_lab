using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class PDList : System.Web.UI.Page
    {
        DAL dal = new DAL();

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
                    if (Request.QueryString["UserType"] != null)
                    {
                        hdnUserType.Value = Request.QueryString["UserType"].ToString();
                    }
                    else
                    {
                        hdnUserType.Value = "";
                    }

                    if (Request.QueryString["Event"] != null)
                    {
                        ddlDuplicacy.SelectedValue = Request.QueryString["Event"].ToString();
                    }

                    FillINV();

                    CheckAccess();

                    GetData();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        private void CheckAccess()
        {
            try
            {
                if (hdnUserType.Value == "Medical" || hdnUserType.Value == "Stats" || hdnUserType.Value == "CTMS")
                {
                    btnAddNew.Visible = true;
                }
                else
                {
                    btnAddNew.Visible = false;
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
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GET_INVID_SP();
                drp_INVID.DataSource = ds.Tables[0];
                drp_INVID.DataValueField = "INVNAME";
                drp_INVID.DataBind();
                drp_INVID.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }

        }

        public void FillSubject()
        {
            try
            {
                DataSet ds = dal.GET_SUBJECT_SP(INVID: drp_INVID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drp_SUBJID.Items.Clear();
                    drp_SUBJID.DataSource = ds.Tables[0];
                    drp_SUBJID.DataValueField = "SUBJID";
                    drp_SUBJID.DataTextField = "SUBJID";
                    drp_SUBJID.DataBind();
                }
                else
                {
                    drp_SUBJID.Items.Clear();
                }
                drp_SUBJID.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        public void GetData()
        {
            try
            {
                DataSet ds;
                ds = new DataSet();

                //Get MH Data
                ds = new DataSet();


                ds = dal.ProtocolVoilation_SP(
                Action: "GET_DATA_LIST",
                INVID: drp_INVID.SelectedValue,
                SUBJID: drp_SUBJID.SelectedValue,
                DUPLICATE: ddlDuplicacy.SelectedValue,
                Source: hdnUserType.Value
                );

                if (ds != null)
                {
                    grdProtVoil.DataSource = ds.Tables[0];
                    grdProtVoil.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }


        protected void Drp_Project_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
            FillINV();

        }

        protected void drp_INVID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
            FillSubject();
        }

        protected void drp_SUBJID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        protected void grdProtVoil_PreRender(object sender, EventArgs e)
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal.ProtocolVoilation_SP(Action: "ExportExcel", INVID: drp_INVID.SelectedValue, SUBJID: drp_SUBJID.SelectedValue, Source: hdnUserType.Value);
                ds.Tables[0].TableName = "Protocol Deviation Log";
                Multiple_Export_Excel.ToExcel(ds, "Protocol_Deviation_Log.xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlDuplicacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("PDList_AddNew.aspx", false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}