using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class MM_PD_DETAILS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtNOTES_MED.Attributes.Add("MaxLength", "1000");
                txtIMPACT_MED.Attributes.Add("MaxLength", "1000");
                txtNOTES_SPONSOR.Attributes.Add("MaxLength", "1000");
                txtSUMMARY_SPONSOR.Attributes.Add("MaxLength", "1000");

                if (!IsPostBack)
                {
                    GET_PROCESS();
                    GET_CLASSIFICATION_SPONSOR();

                    GET_PD_DETAILS();

                    SHOW_HIDE_FIELDS();

                    GET_RELATED();
                    GET_AUDITTRAIL();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_PD_DETAILS()
        {
            try
            {
                DataSet ds = dal_MM.MM_PD_SP(ACTION: "GET_PD_DETAILS", PD_ID: Request.QueryString["PD_ID"].ToString());
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtTranspose = GenerateTransposedTable(ds.Tables[0]);
                    lstPDdetails.DataSource = dtTranspose;
                    lstPDdetails.DataBind();

                }
                else
                {
                    lstPDdetails.DataSource = null;
                    lstPDdetails.DataBind();
                }

                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    ddlPROCESS.SelectedValue = ds.Tables[1].Rows[0]["PROCESS"].ToString();
                    GET_CATEGORY();
                    ddlCAT.SelectedValue = ds.Tables[1].Rows[0]["CAT"].ToString();
                    GET_SUBCATEGORY();
                    ddlSUBCAT.SelectedValue = ds.Tables[1].Rows[0]["SUBCAT"].ToString();
                    GET_CLASSIFICATION();
                    ddlCLASS_MED.SelectedValue = ds.Tables[1].Rows[0]["CLASS_MED"].ToString();
                    txtNOTES_MED.Text = ds.Tables[1].Rows[0]["NOTES_MED"].ToString();
                    txtIMPACT_MED.Text = ds.Tables[1].Rows[0]["IMPACT_MED"].ToString();
                    ddlCLASS_SPONSOR.SelectedValue = ds.Tables[1].Rows[0]["CLASS_SPONSOR"].ToString();
                    txtNOTES_SPONSOR.Text = ds.Tables[1].Rows[0]["NOTES_SPONSOR"].ToString();
                    txtSUMMARY_SPONSOR.Text = ds.Tables[1].Rows[0]["SUMMARY_SPONSOR"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("NAME");
            outputTable.Columns.Add("VAL");

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                foreach (DataColumn dc in inputTable.Columns)
                {
                    DataRow drNew = outputTable.NewRow();
                    drNew["NAME"] = dc.ColumnName.ToString();
                    drNew["VAL"] = inputTable.Rows[i][dc.ColumnName];
                    outputTable.Rows.Add(drNew);
                }
            }

            return outputTable;
        }

        private void SHOW_HIDE_FIELDS()
        {
            if (Session["UserType"].ToString() == "Sponsor")
            {
                ddlPROCESS.Enabled = false;
                ddlCAT.Enabled = false;
                ddlSUBCAT.Enabled = false;
                ddlCLASS_MED.Enabled = false;
                txtNOTES_MED.Enabled = false;
                txtIMPACT_MED.Enabled = false;

                ddlCLASS_SPONSOR.Enabled = true;
                ddlCLASS_SPONSOR.CssClass = "form-control required";
                txtSUMMARY_SPONSOR.Enabled = true;
                txtSUMMARY_SPONSOR.CssClass = "form-control required";
                txtNOTES_SPONSOR.Enabled = true;
            }
            else if (Session["UserType"].ToString() == "Internal")
            {
                ddlPROCESS.Enabled = true;
                ddlPROCESS.CssClass = "form-control required";
                ddlCAT.Enabled = true;
                ddlCAT.CssClass = "form-control required";
                ddlSUBCAT.Enabled = true;
                ddlSUBCAT.CssClass = "form-control required";
                ddlCLASS_MED.Enabled = true;
                ddlCLASS_MED.CssClass = "form-control required";
                txtIMPACT_MED.Enabled = true;
                txtIMPACT_MED.CssClass = "form-control required";
                txtNOTES_MED.Enabled = true;

                txtNOTES_SPONSOR.Enabled = false;
                txtSUMMARY_SPONSOR.Enabled = false;
                ddlCLASS_SPONSOR.Enabled = false;
            }

        }

        private void GET_PROCESS()
        {
            try
            {
                DataSet ds = dal_MM.MM_PD_SP(ACTION: "GET_PROCESS");
                ddlPROCESS.DataSource = ds.Tables[0];
                ddlPROCESS.DataValueField = "Process";
                ddlPROCESS.DataBind();
                ddlPROCESS.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_CATEGORY()
        {
            try
            {
                DataSet ds = dal_MM.MM_PD_SP(ACTION: "GET_CATEGORY", PROCESS: ddlPROCESS.SelectedValue);
                ddlCAT.DataSource = ds.Tables[0];
                ddlCAT.DataValueField = "Category";
                ddlCAT.DataBind();
                ddlCAT.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_SUBCATEGORY()
        {
            try
            {
                DataSet ds = dal_MM.MM_PD_SP(ACTION: "GET_SUBCATEGORY", PROCESS: ddlPROCESS.SelectedValue, CAT: ddlCAT.SelectedValue);
                ddlSUBCAT.DataSource = ds.Tables[0];
                ddlSUBCAT.DataValueField = "SubCategory";
                ddlSUBCAT.DataBind();
                ddlSUBCAT.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_CLASSIFICATION()
        {
            try
            {
                DataSet ds = dal_MM.MM_PD_SP(ACTION: "GET_CLASSIFICATION", PROCESS: ddlPROCESS.SelectedValue, CAT: ddlCAT.SelectedValue, SUBCAT: ddlSUBCAT.SelectedValue);
                lblCLASS.Text = ds.Tables[0].Rows[0]["Classification"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_CLASSIFICATION_SPONSOR()
        {
            try
            {
                DataSet ds = dal_MM.MM_PD_SP(ACTION: "GET_CLASSIFICATION_SPONSOR");
                ddlCLASS_SPONSOR.DataSource = ds.Tables[0];
                ddlCLASS_SPONSOR.DataValueField = "Classification";
                ddlCLASS_SPONSOR.DataBind();
                ddlCLASS_SPONSOR.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlCLASS_MED.DataSource = ds.Tables[0];
                ddlCLASS_MED.DataValueField = "Classification";
                ddlCLASS_MED.DataBind();
                ddlCLASS_MED.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ddlProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_CATEGORY();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBCATEGORY();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_CLASSIFICATION();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dal_MM.MM_PD_SP(
                    ACTION: "SUBMIT_PD",
                    PD_ID: Request.QueryString["PD_ID"].ToString(),
                    PROCESS: ddlPROCESS.SelectedValue,
                    CAT: ddlCAT.SelectedValue,
                    SUBCAT: ddlSUBCAT.SelectedValue,
                    CLASS_MED: ddlCLASS_MED.SelectedValue,
                    NOTES_MED: txtNOTES_MED.Text,
                    IMPACT_MED: txtIMPACT_MED.Text,
                    CLASS_SPONSOR: ddlCLASS_SPONSOR.SelectedValue,
                    NOTES_SPONSOR: txtNOTES_SPONSOR.Text,
                    SUMMARY_SPONSOR: txtSUMMARY_SPONSOR.Text
                    );

                Response.Redirect("MM_PD_LIST.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("MM_PD_LIST.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
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

        protected void grdPROTVIOL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grdPROTVIOL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    Response.Redirect("MM_PD_DETAILS.aspx?PD_ID=" + e.CommandArgument);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_RELATED()
        {
            try
            {
                DataSet dsRelated = dal_MM.MM_PD_SP(ACTION: "GET_RELATED_PD", PD_ID: Request.QueryString["PD_ID"].ToString());
                if (dsRelated.Tables.Count > 0)
                {
                    grdRelated.DataSource = dsRelated.Tables[0];
                    grdRelated.DataBind();

                    if (dsRelated.Tables.Count > 1)
                    {
                        grdSiteRelated.DataSource = dsRelated.Tables[1];
                        grdSiteRelated.DataBind();
                    }

                    if (dsRelated.Tables.Count > 2)
                    {
                        grdSubjectRelated.DataSource = dsRelated.Tables[2];
                        grdSubjectRelated.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AUDITTRAIL()
        {
            try
            {
                DataSet dsAuditTrail = dal_MM.MM_PD_SP(ACTION: "GET_AUDITTRAIL_PD", PD_ID: Request.QueryString["PD_ID"].ToString());
                if (dsAuditTrail.Tables.Count > 0)
                {
                    grdAuditTrail.DataSource = dsAuditTrail.Tables[0];
                    grdAuditTrail.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



    }
}