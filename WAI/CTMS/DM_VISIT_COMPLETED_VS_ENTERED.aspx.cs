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
    public partial class DM_VISIT_COMPLETED_VS_ENTERED : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
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
            DAL dal = new DAL();

            DataSet ds = dal.GET_INVID_SP(
                    USERID: Session["User_ID"].ToString()
                    );
            ddlSite.DataSource = ds.Tables[0];
            ddlSite.DataValueField = "INVID";
            ddlSite.DataBind();

            FillSubject();
        }

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();

                gridData.DataSource = null;
                gridData.DataBind();
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
                DataSet ds = dal_DM.DM_SUBJECT_LIST_SP(INVID: ddlSite.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSUBJID.DataSource = ds.Tables[0];
                    ddlSUBJID.DataValueField = "SUBJID";
                    ddlSUBJID.DataTextField = "SUBJID";
                    ddlSUBJID.DataBind();
                    ddlSUBJID.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    ddlSUBJID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ddlSUBJID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillVisit();

                gridData.DataSource = null;
                gridData.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void FillVisit()
        {
            try
            {
                DataSet ds = dal_DM.DM_VISIT_SP(SUBJID: ddlSUBJID.SelectedValue);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    if (dr["CritID"].ToString() != "" && dr["CritID"].ToString() != "0" && !toBeDeleted_Visit(dr["CritID"].ToString()) && ddlSUBJID.SelectedValue != "Select")
                    {
                        dr.Delete();
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVisit.DataSource = ds.Tables[0];
                    ddlVisit.DataValueField = "VISITNUM";
                    ddlVisit.DataTextField = "VISIT";
                    ddlVisit.DataBind();
                    ddlVisit.Items.Insert(0, new ListItem("--Select Visit--", "Select"));
                }
                else
                {
                    ddlVisit.Items.Clear();
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
                DataSet ds = dal_DM.DM_VISIT_CRITERIA_SP(
                ID: CritID,
                SUBJID: ddlSUBJID.SelectedValue,
                SITEID: ddlSite.SelectedValue
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

        protected void ddlVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OpenCRF();

                gridData.DataSource = null;
                gridData.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void OpenCRF()
        {
            try
            {
                DataSet ds = dal_DM.DM_OPEN_CRF_SP(
                    SUBJID: ddlSUBJID.SelectedValue,
                    VISITNUM: ddlVisit.SelectedValue
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
                    ddlModule.DataSource = ds.Tables[0];
                    ddlModule.DataValueField = "MODULEID";
                    ddlModule.DataTextField = "MODULENAME";
                    ddlModule.DataBind();
                    ddlModule.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    ddlModule.Items.Clear();
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
                DataSet ds = dal_DM.DM_MODULE_CRITERIA_SP(
                ID: CritID,
                SUBJID: ddlSUBJID.SelectedValue,
                VISITNUM: ddlVisit.SelectedValue,
                SITEID: ddlSite.SelectedValue
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

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gridData.DataSource = null;
                gridData.DataBind();
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
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DATA()
        {
            try
            {
                DataSet ds = dal_DM.DM_VISIT_COMPLETED_VS_SDV_REPORT(
                    INVID: ddlSite.SelectedValue,
                    SUBJID: ddlSUBJID.SelectedValue,
                    VISITNUM: ddlVisit.SelectedValue,
                    MODULEID: ddlModule.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = ds;
                    gridData.DataBind();
                }
                else
                {
                    gridData.DataSource = null;
                    gridData.DataBind();
                }
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

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_DM.DM_VISIT_COMPLETED_VS_SDV_REPORT(
                    INVID: ddlSite.SelectedValue,
                    SUBJID: ddlSUBJID.SelectedValue,
                    VISITNUM: ddlVisit.SelectedValue,
                    MODULEID: ddlModule.SelectedValue
                    );

                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}