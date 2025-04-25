
using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class NSAE_SETUP_REPORT : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_DEFINEREPORT();
                    GET_REPORT();
                    GET_SAE_MODULE();
                    GET_REPORT_VARIABLES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        private void GET_REPORT()
        {
            DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_REPORT");
            if (ds.Tables[0].Rows.Count > 0)
            {
                drpReport.DataSource = ds.Tables[0];
                drpReport.DataValueField = "ID";
                drpReport.DataTextField = "REPORT_NAME";
                drpReport.DataBind();
                drpReport.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                drpReport.DataSource = null;
                drpReport.DataBind();
            }
        }

        private void GET_SAE_MODULE()
        {
            DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_SAE_MODULE");
            if (ds.Tables[0].Rows.Count > 0)
            {
                drpModule.DataSource = ds.Tables[0];
                drpModule.DataValueField = "MODULEID";
                drpModule.DataTextField = "MODULENAME";
                drpModule.DataBind();
                drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                drpModule.DataSource = null;
                drpModule.DataBind();
            }
        }

        private void GET_SAE_FIELDS()
        {
            DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_SAE_FIELDS", MODULEID: drpModule.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstFeilds.DataSource = ds.Tables[0];
                lstFeilds.DataValueField = "FIELD_ID";
                lstFeilds.DataTextField = "FIELDNAME";
                lstFeilds.DataBind();
            }
            else
            {
                lstFeilds.DataSource = null;
                lstFeilds.DataBind();
            }
        }

        private void GET_DEFINEREPORT()
        {
            DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_DEFINEREPORT");
            if (ds.Tables[0].Rows.Count > 0)
            {
                grddefineReport.DataSource = ds.Tables[0];
                grddefineReport.DataBind();

            }
            else
            {
                grddefineReport.DataSource = null;
                grddefineReport.DataBind();
            }

        }

        private void CLEAR()
        {
            txtReportName.Text = "";
            chkInternal.Checked = false;
            chkSite.Checked = false;

            btnSubmitDefineReport.Visible = true;
            btnUpdateDefineReport.Visible = false;


        }

        private void INSERT_REPORT()
        {
            try
            {

                string fileName = DfnReportFile.FileName;
                string contentType = DfnReportFile.PostedFile.ContentType;
                string fileExtension = Path.GetExtension(fileName).ToLower();
                int fileSize = DfnReportFile.PostedFile.ContentLength;

                byte[] fileData;
                using (Stream stream = DfnReportFile.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();

                    }
                }
                if (fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".docm")
                {
                    DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "INSERT_SAE_DEFINEREPORT",
                        REPORT_NAME: txtReportName.Text,
                        INTERNAL: chkInternal.Checked,
                        SITE: chkSite.Checked,
                        FILENAME: fileName,
                        CONTENT_TYPE: contentType,
                        DATA_TYPE: fileData,
                        SIZE: fileSize.ToString()
                        );

                    Response.Write("<script>alert('File Uploaded Successfully.');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please Select Word File Only.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnSubmitDefineReport_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_REPORT();
                GET_DEFINEREPORT();
                CLEAR();
                GET_REPORT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnUpdateDefineReport_Click(object sender, EventArgs e)
        {
            UPDATE_DEFINEREPORT();
            CLEAR();
            GET_DEFINEREPORT();
            GET_REPORT();
        }

        protected void btnCancelDefineReport_Click(object sender, EventArgs e)
        {
            CLEAR();
            Response.Redirect("NSAE_SETUP_REPORT.aspx");
        }

        private void EDIT_DEFINEREPORT(string ID)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "EDIT_DEFINEREPORT", ID: ID);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtReportName.Text = ds.Tables[0].Rows[0]["REPORT_NAME"].ToString();
                    string Internal = ds.Tables[0].Rows[0]["INTERNAL"].ToString();
                    if (Internal == "True")
                    {
                        chkInternal.Checked = true;
                    }
                    else
                    {
                        chkInternal.Checked = false;
                    }
                    string Site = ds.Tables[0].Rows[0]["SITE"].ToString();
                    if (Site == "True")
                    {
                        chkSite.Checked = true;
                    }
                    else
                    {
                        chkSite.Checked = false;
                    }
                }
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void DELETE_DEFINEREPORT(string ID)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "DELETE_DEFINEREPORT", ID: ID);

                Response.Write("<script>alert('Define Report Deleted Successfully.');</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void UPDATE_DEFINEREPORT()
        {
            try
            {
                string fileName = DfnReportFile.FileName;
                string contentType = DfnReportFile.PostedFile.ContentType;
                string fileExtension = Path.GetExtension(fileName).ToLower();
                int fileSize = DfnReportFile.PostedFile.ContentLength;
                byte[] fileData;
                using (Stream stream = DfnReportFile.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();

                    }
                }
                if (fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".docm")
                {
                    DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "UPDATE_DEFINEREPORT", ID: Session["ID"].ToString(),
                        REPORT_NAME: txtReportName.Text,
                        INTERNAL: chkInternal.Checked,
                        SITE: chkSite.Checked,
                        FILENAME: fileName,
                        CONTENT_TYPE: contentType,
                        DATA_TYPE: fileData,
                        SIZE: fileSize.ToString()
                        );

                    Response.Write("<script>alert('Data Updated Successfully.');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please Select Word File Only.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grddefineReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string ID = e.CommandArgument.ToString();
            Session["ID"] = ID;
            if (e.CommandName == "EditDefineReport")
            {
                EDIT_DEFINEREPORT(ID);
                btnSubmitDefineReport.Visible = false;
                btnUpdateDefineReport.Visible = true;
            }
            else if (e.CommandName == "DeleteDefineReport")
            {
                DELETE_DEFINEREPORT(ID);
                GET_DEFINEREPORT();
                GET_REPORT();
            }
        }

        protected void drpControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstFeilds.ClearSelection();
            if (drpControlType.SelectedValue == "Repeating Section Content Control")
            {
                lstFeilds.SelectionMode = ListSelectionMode.Multiple;

            }
            else
            {
                lstFeilds.SelectionMode = ListSelectionMode.Single;
            }
        }

        protected void btnSubmitVaribales_Click(object sender, EventArgs e)
        {
            INSERT_REPORT_VARIABLES();
            GET_REPORT_VARIABLES();
            GetClear();
        }

        private void GET_REPORT_VARIABLES()
        {
            DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_REPORT_VARIABLES", REPORT_ID: drpReport.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                grdReportVariavles.DataSource = ds.Tables[0];
                grdReportVariavles.DataBind();

            }
            else
            {
                grdReportVariavles.DataSource = null;
                grdReportVariavles.DataBind();
            }
        }

        private void INSERT_REPORT_VARIABLES()
        {
            try
            {
                string FIELDSIDS = null;

                foreach (ListItem item in lstFeilds.Items)
                {
                    if (item.Selected == true)
                    {
                        if (FIELDSIDS != null)
                        {
                            FIELDSIDS += "," + item.Value.ToString();
                        }
                        else
                        {
                            FIELDSIDS += item.Value.ToString();
                        }
                    }
                }

                DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "INSERT_REPORT_VARIABLES",
                                    REPORT_ID: drpReport.SelectedValue,
                                    CONTENT_TYPE: drpControlType.SelectedValue,
                                    SAE_MODULEID: drpModule.SelectedValue,
                                    SAE_FIELDID: FIELDSIDS
                            );
                Response.Write("<script>alert('Add Report Variables Successfully.');</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnUpdateVaribales_Click(object sender, EventArgs e)
        {
            UPDATE_REPORT_VARIABLE();
            GetClear();
            GET_REPORT_VARIABLES();
        }
        private void UPDATE_REPORT_VARIABLE()
        {
            try
            {
                string FIELDSIDS = null;

                foreach (ListItem item in lstFeilds.Items)
                {
                    if (item.Selected == true)
                    {
                        if (FIELDSIDS != null)
                        {
                            FIELDSIDS += "," + item.Value.ToString();
                        }
                        else
                        {
                            FIELDSIDS += item.Value.ToString();
                        }
                    }
                }

                DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "UPDATE_REPORT_VARIABLE",
                                    REPORT_ID: drpReport.SelectedValue,
                                    CONTENT_TYPE: drpControlType.SelectedValue,
                                    SAE_MODULEID: drpModule.SelectedValue,
                                    SAE_FIELDID: FIELDSIDS,
                                    ID: Session["ID"].ToString()
                            );
                Response.Write("<script>alert('Update Report Variables Successfully.');</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCancelVaribales_Click(object sender, EventArgs e)
        {
            GetClear();
            Response.Redirect("NSAE_SETUP_REPORT.aspx");

        }

        private void GetClear()
        {
            drpModule.ClearSelection();
            drpReport.ClearSelection();
            drpControlType.ClearSelection();
            drpReport.ClearSelection();
            lstFeilds.ClearSelection();
            btnUpdateVaribales.Visible = false;
            btnSubmitVaribales.Visible = true;
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            GET_SAE_FIELDS();
        }

        protected void grdReportVariavles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string ID = dr["FIELD_IDS"].ToString();


                    GridView grdListReportVariable = (GridView)e.Row.FindControl("grdListReportVariable");
                    DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_LISTREPORTVARIABLE", SAE_FIELDID: ID);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdListReportVariable.DataSource = ds.Tables[0];
                        grdListReportVariable.DataBind();
                    }
                    else
                    {
                        grdListReportVariable.DataSource = null;
                        grdListReportVariable.DataBind();
                    }


                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdReportVariavles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            Session["ID"] = ID;
            if (e.CommandName == "EditReportVariable")
            {
                EDIT_REPORT_VARIABLE(ID);
                btnSubmitVaribales.Visible = false;
                btnUpdateVaribales.Visible = true;
            }
            else if (e.CommandName == "DeleteReportVariable")
            {

                DELETE_REPORT_VARIABLE(ID);
                GetClear();
                GET_REPORT_VARIABLES();
            }
        }

        private void EDIT_REPORT_VARIABLE(string ID)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "EDIT_REPORT_VARIABLE", ID: ID);
                drpReport.SelectedValue = ds.Tables[0].Rows[0]["REPORT_ID"].ToString();
                drpControlType.SelectedValue = ds.Tables[0].Rows[0]["CONTROL_TYPE"].ToString();
                drpModule.SelectedValue = ds.Tables[0].Rows[0]["MODULE_ID"].ToString();
                GET_SAE_FIELDS();
                if (drpControlType.SelectedValue == "Repeating Section Content Control")
                {
                    lstFeilds.SelectionMode = ListSelectionMode.Multiple;

                }
                else
                {
                    lstFeilds.SelectionMode = ListSelectionMode.Single;
                }
                string[] fieldArr = ds.Tables[0].Rows[0]["FIELD_IDS"].ToString().Split(',');

                if (fieldArr.Length > 0)
                {
                    for (int i = 0; i < fieldArr.Length; i++)
                    {
                        lstFeilds.Items.FindByValue(fieldArr[i]).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_REPORT_VARIABLE(string ID)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "DELETE_REPORT_VARIABLE", ID: ID);

                Response.Write("<script>alert('Define Variable Deleted Successfully.');</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grddefineReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    HtmlControl iconInternal = (HtmlControl)e.Row.FindControl("iconInternal");
                    if (drv["Internal"].ToString() == "True")
                    {
                        iconInternal.Attributes.Add("class", "fa fa-check");
                        iconInternal.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconInternal.Attributes.Add("class", "fa fa-times");
                        iconInternal.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconSite = (HtmlControl)e.Row.FindControl("iconSite");
                    if (drv["Site"].ToString() == "True")
                    {
                        iconSite.Attributes.Add("class", "fa fa-check");
                        iconSite.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSite.Attributes.Add("class", "fa fa-times");
                        iconSite.Attributes.Add("style", "color: red;");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grddefineReport_PreRender(object sender, EventArgs e)
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

        protected void drpReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_REPORT_VARIABLES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }
    }
}