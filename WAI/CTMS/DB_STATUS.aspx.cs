using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DB_STATUS : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    GET_MIGRATION();
                    GetStatusLogs();
                }
                GET_MIGRATION();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MIGRATION()
        {
            try
            {
                DataSet ds = dal_DB.DB_STATUS_SP(ACTION: "GET_MIGRATION");


                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblversion.Text = ds.Tables[0].Rows[0]["DB_VERSION"].ToString();
                    lblverion.Text = lblversion.Text;
                    lblver.Text = lblverion.Text;
                    if (ds.Tables[0].Rows[0]["MIGRATION"].ToString() == "False")
                    {
                        div1.Visible = true;
                    }
                    else
                    {
                        div2.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnmigrate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_STATUS_SP(ACTION: "GET_LIST_MODULE");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    divgrid.Visible = true;

                    grd_data.DataSource = ds.Tables[0];
                    grd_data.DataBind();

                }
                else
                {
                    dal_DB.DB_STATUS_SP(ACTION: "UPDATE_MIGRATION");

                    Response.Write("<script> alert('DB migration completed successfully.');window.location='DB_STATUS.aspx'; </script>");
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

        protected void GetStatusLogs()
        {
            try
            {
                DataSet ds = dal_DB.DB_STATUS_SP(ACTION: "GET_DB_STATUS_LOGS_REC");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gridstatuslogs.DataSource = ds.Tables[0];
                    gridstatuslogs.DataBind();
                }
                else
                {
                    gridstatuslogs.DataSource = null;
                    gridstatuslogs.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridstatuslogs_PreRender(object sender, EventArgs e)
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
        protected void UnlockDBVersion_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                dal_DB.DB_STATUS_SP(
                    ACTION: "UPDATE_DB_VERSION",
                    DB_VERSION: lblverion.Text,
                    LAST_DB_VERSION: lblverion.Text
                    );

                Response.Write("<script> alert('DB Version migrated successfully.');window.location='DB_STATUS.aspx'; </script>");
            }
            catch (Exception ex)
            {

                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();

            divNo.Visible = true;
            btnSubmit.Visible= true;
            btnback.Visible = true;
            lbnote.Visible = false;
            btnYes.Visible = false;
            btnNo.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dal_DB.DB_STATUS_SP(
                    ACTION: "UPDATE_DB_VERSION",
                    DB_VERSION: txtVesion.Text.Trim(),
                    LAST_DB_VERSION: lblverion.Text
                    );

                Response.Write("<script> alert('DB Version defined successfully.');window.location='DB_STATUS.aspx'; </script>");

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
            divNo.Visible = false;
            btnSubmit.Visible = false;
            btnback.Visible = false;
            lbnote.Visible = true;
            btnYes.Visible = true;
            btnNo.Visible = true;
        }

        protected void btnclosed_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
        }
    }
}