using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_LABS_DATA : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!IsPostBack)
                {
                    FillINV();
                    GET_TestName();
                    GET_Gender();
                    GET_LAB();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            DAL dal = new DAL();

            DataSet ds = dal.GET_INVID_SP(USERID: Session["User_ID"].ToString());

            ddlSITE.DataSource = ds.Tables[0];
            ddlSITE.DataValueField = "INVID";
            ddlSITE.DataBind();
            ddlSITE.Items.Insert(0, new ListItem("--Select--", "0"));


        }

        protected void ddlSITE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CANCEL_LAB();
                GET_LabName();
                GET_LAB();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LabName()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(
                    ACTION: "GET_LAB",
                    SITEID: ddlSITE.SelectedValue
                    );

                ddlLabName.DataSource = ds.Tables[0];
                ddlLabName.DataValueField = "LABID";
                ddlLabName.DataTextField = "LABNAME";
                ddlLabName.DataBind();
                ddlLabName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlLabName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_LAB();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_TestName()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(
                ACTION: "GET_TestName"
                );

                ddlTestName.DataSource = ds.Tables[0];
                ddlTestName.DataValueField = "VALUE";
                ddlTestName.DataTextField = "TEXT";
                ddlTestName.DataBind();
                ddlTestName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlTestName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_LAB();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_Gender()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(
                ACTION: "GET_Gender"
                );

                ddlGender.DataSource = ds.Tables[0];
                ddlGender.DataValueField = "VALUE";
                ddlGender.DataTextField = "TEXT";
                ddlGender.DataBind();
                ddlGender.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlGender.Items.Insert(1, new ListItem("Both", "Both"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_LAB();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LAB()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_LABDATA",
                    SITEID: ddlSITE.SelectedValue,
                    LABID: ddlLabName.SelectedValue,
                    TESTNAME: ddlTestName.SelectedValue,
                    SEX: ddlGender.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdLabsData.DataSource = ds;
                    grdLabsData.DataBind();
                    divRecords.Visible = true;
                    lbtnExport.Visible = true;

                }
                else
                {
                    grdLabsData.DataSource = null;
                    grdLabsData.DataBind();
                    divRecords.Visible = false;
                    lbtnExport.Visible = false;
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
                INSERT_LAB();
                CANCEL_LAB();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lab Reference Range added successfully.'); window.location.reload();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_LAB()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "INSERT_LAB_DEFAULT_DATA",
                  SITEID: ddlSITE.SelectedValue,
                  LABID: ddlLabName.SelectedValue,
                  TESTNAME: ddlTestName.SelectedValue,
                  SEX: ddlGender.SelectedValue,
                  AGELOW: txtAgeLower.Text,
                  AGEHI: txtAgeUpper.Text,
                  REFRANGELO: txtRefLower.Text,
                  REFRANGEHI: txtRefUpper.Text,
                  UNIT: txtUnits.Text,
                  FROMDATE: txtfromdate.Text,
                  ENDDATE: txtTodate.Text
                  );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_LAB();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_LAB()
        {
            try
            {
                dal_DB.DB_LAB_REFERENCE_SP(ACTION: "Update_LAB_DEFAULT_DATA",
                  SITEID: ddlSITE.SelectedValue,
                  LABID: ddlLabName.SelectedValue,
                  TESTNAME: ddlTestName.SelectedValue,
                  SEX: ddlGender.SelectedValue,
                  AGELOW: txtAgeLower.Text,
                  AGEHI: txtAgeUpper.Text,
                  REFRANGELO: txtRefLower.Text,
                  REFRANGEHI: txtRefUpper.Text,
                  UNIT: txtUnits.Text,
                  FROMDATE: txtfromdate.Text,
                  ENDDATE: txtTodate.Text,
                  ID: ViewState["DM_DEFUALT_ID"].ToString()
                );

                CANCEL_LAB();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lab Reference Range updated successfully.'); window.location.reload();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl.ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdLabsData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditLab")
                {
                    SELECT_LAB(e.CommandArgument.ToString());
                    divRefRange.Focus();
                }
                else if (e.CommandName == "DeleteLab")
                {
                    DELETE_LAB(e.CommandArgument.ToString());

                    GET_LAB();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_LAB(string ID)
        {
            try
            {
                ViewState["DM_DEFUALT_ID"] = ID;

                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_LABDATA_BYID", ID: ID);

                FillINV();
                ddlSITE.SelectedValue = ds.Tables[0].Rows[0]["SITEID"].ToString();

                GET_LabName();
                ddlLabName.SelectedValue = ds.Tables[0].Rows[0]["LABID"].ToString();

                GET_TestName();
                ddlTestName.SelectedValue = ds.Tables[0].Rows[0]["LBTEST"].ToString();

                GET_Gender();
                ddlGender.SelectedValue = ds.Tables[0].Rows[0]["SEX"].ToString();

                txtAgeLower.Text = ds.Tables[0].Rows[0]["AGELO"].ToString();
                txtAgeUpper.Text = ds.Tables[0].Rows[0]["AGEHI"].ToString();
                txtRefLower.Text = ds.Tables[0].Rows[0]["LBORNRLO"].ToString();
                txtRefUpper.Text = ds.Tables[0].Rows[0]["LBORNRHI"].ToString();
                txtUnits.Text = ds.Tables[0].Rows[0]["LBORRESU"].ToString();
                txtfromdate.Text = ds.Tables[0].Rows[0]["EFFDATEFROM"].ToString();
                txtTodate.Text = ds.Tables[0].Rows[0]["EFFDATETO"].ToString();

                ddlSITE.Enabled = false;
                ddlLabName.Enabled = false;
                ddlTestName.Enabled = false;
                ddlGender.Enabled = false;
                txtTodate.Enabled = false;
                txtfromdate.Enabled = false;
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_LAB(string ID)
        {
            try
            {
                dal_DB.DB_LAB_REFERENCE_SP(ACTION: "DELETE_LAB_DEFAULT_DATA", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CANCEL_LAB()
        {
            try
            {
                txtAgeLower.Text = "";
                txtAgeUpper.Text = "";
                txtRefLower.Text = "";
                txtRefUpper.Text = "";
                txtUnits.Text = "";
                txtfromdate.Text = "";
                txtTodate.Text = "";

                btnSubmit.Visible = true;
                btnUpdate.Visible = false;

                GET_LAB();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_LAB_REF_EXPORT",
                    SITEID: ddlSITE.SelectedValue,
                    LABID: ddlLabName.SelectedValue,
                    TESTNAME: ddlTestName.SelectedValue,
                    SEX: ddlGender.SelectedValue
                    );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_ Lab Reference Range Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}