using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class Code_Mismatch : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    BIND_FORM();
                    GET_SITE();

                    if (Session["CODE_MODULEID"] == null && Session["CODE_SITEID"] == null && Session["CODE_SUBJID"] == null)
                    {
                        drpForm.SelectedValue = "";
                        ddlSUBJID.SelectedValue = "";
                        ddlSite.SelectedValue = "";
                    }
                    else
                    {
                        drpForm.SelectedValue = Session["CODE_MODULEID"].ToString();
                        GET_SITE();
                        ddlSite.SelectedValue = Session["CODE_SITEID"].ToString();
                        GET_SUBJECTS();
                        ddlSUBJID.SelectedValue = Session["CODE_SUBJID"].ToString();
                        SITESUBJ.Visible = true;
                        divSIte.Visible = true;
                        divSubject.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_FORM()
        {
            try
            {
                DataSet dt = dal.DB_CODE_SP(
                       ACTION: "GET_MODULENAME"
                       );

                if (dt.Tables[0].Rows.Count > 0)
                {
                    drpForm.DataSource = dt;
                    drpForm.DataTextField = "MODULENAME";
                    drpForm.DataValueField = "ID";
                    drpForm.DataBind();
                    drpForm.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                        ddlSite.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                    else
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                    }
                }
                else
                {
                    ddlSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBJECTS()
        {
            try
            {
                DataSet ds = dal.GET_SUBJECT_SP(INVID: ddlSite.SelectedValue);
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

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBJECTS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpForm.SelectedValue != "0")
                {
                    SITESUBJ.Visible = true;
                    divSIte.Visible = true;
                    divSubject.Visible = true;
                }
                else
                {
                    SITESUBJ.Visible = false;
                    divSIte.Visible = false;
                    divSubject.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
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

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetData()
        {
            try
            {
                DataSet dt = dal.DB_CODE_SP(
                ACTION: "GET_MISMATCH_DATA",
                MODULEID: drpForm.SelectedValue,
                SUBJID: ddlSUBJID.SelectedValue,
                SITEID: ddlSite.SelectedValue
                );

                Session["CODE_MODULEID"] = drpForm.SelectedValue;
                Session["CODE_SUBJID"] = ddlSUBJID.SelectedValue;
                Session["CODE_SITEID"] = ddlSite.SelectedValue;

                if (dt.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = dt;
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

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[3].CssClass = "disp-none";
                e.Row.Cells[4].CssClass = "disp-none";
                e.Row.Cells[5].CssClass = "disp-none";

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbtnShow = (LinkButton)e.Row.FindControl("lbtnShow");

                    Label PVID = (Label)e.Row.FindControl("PVID");
                    Label RECID = (Label)e.Row.FindControl("RECID");
                    string MODULEID = drpForm.SelectedValue;

                    DataSet ds = dal.DB_CODE_SP(
                    ACTION: "GET_MISMATCH_TERMS",
                    MODULEID: MODULEID,
                    PVID: PVID.Text,
                    RECID: RECID.Text
                    );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lbtnShow.Visible = true;
                    }
                    else
                    {
                        lbtnShow.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Session["CODE_MODULEID"] = drpForm.SelectedValue;
                Session["CODE_SUBJID"] = ddlSUBJID.SelectedValue;
                Session["CODE_SITEID"] = ddlSite.SelectedValue;

                string MODULEID = drpForm.SelectedValue;
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Label PVID = (Label)gridData.Rows[index].FindControl("PVID");
                Label RECID = (Label)gridData.Rows[index].FindControl("RECID");

                if (e.CommandName == "ChangeCode")
                {
                    Response.Redirect("Code_ManualCoding.aspx?MODULEID=" + MODULEID + "&PVID=" + PVID.Text + "&RECID=" + RECID.Text + "&ACTION=ChangeCode");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        [System.Web.Services.WebMethod]
        public static string showCodeMismatches(string PVID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                DAL dal = new DAL();

                DataSet ds = dal.DB_CODE_SP(
                ACTION: "GET_MISMATCH_TERMS",
                MODULEID: MODULEID,
                PVID: PVID,
                RECID: RECID
                );

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return str;
        }


        public static string ConvertDataTableToHTML(DataSet ds)
        {
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</th>";
            html += "</tr>";

            //add rows
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                html += "<tr style='text-align: left; word-break: break-all;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

    }
}