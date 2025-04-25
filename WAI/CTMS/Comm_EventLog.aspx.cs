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
    public partial class Comm_EventLog : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", false);
                }
                if (!Page.IsPostBack)
                {
                    DataSet ds = dal.Communication_SP(Action: "Get_EventLog", PROJECTID: Session["PROJECTID"].ToString(), UserID: Session["USER_ID"].ToString());
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            grdLog.DataSource = ds.Tables[0];
                            grdLog.DataBind();
                        }
                    }
                    fill_Origin();
                    fill_Dept();
                    Fill_USER();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Origin()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "GET_ORIGINS");

                drpOrigin.DataSource = ds;
                drpOrigin.DataValueField = "origin";
                drpOrigin.DataBind();
                drpOrigin.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Dept()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Dept");

                drpDepartment.Items.Clear();
                drpDepartment.DataSource = ds;
                drpDepartment.DataTextField = "Dept_Name";
                drpDepartment.DataValueField = "Dept_ID";
                drpDepartment.DataBind();
                drpDepartment.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Event()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "Get_Event", Dept_Name: drpDepartment.SelectedValue);

                drpEvent.DataSource = ds;
                drpEvent.DataValueField = "Event";
                drpEvent.DataBind();

                drpEvent.Items.Insert(0, new ListItem("--All--", "0"));
                drpEvent.Items.Insert(1, new ListItem("Not In List", "Not In List"));
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


        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Event();
                GetEventLogs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GetEventLogs()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "Get_EventLog_Filter_Data", PROJECTID: Session["PROJECTID"].ToString(), UserID: drpUserName.SelectedValue, ORIGINS: drpOrigin.SelectedValue, Type: drpType.SelectedValue, Nature: drpNature.SelectedValue, Reference: drpReference.SelectedValue, Department: drpDepartment.SelectedItem.Text, Event: drpEvent.SelectedValue,FromID:txtFromDate.Text,ToID:txtTillDate.Text);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdLog.DataSource = ds.Tables[0];
                        grdLog.DataBind();
                    }
                    else
                    {
                        grdLog.DataSource = null;
                        grdLog.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }


        private void Fill_USER()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "GET_USERS");

                drpUserName.DataSource = ds;
                drpUserName.DataTextField = "User_Name";
                drpUserName.DataValueField = "User_ID";
                drpUserName.DataBind();
                drpUserName.Items.Insert(0, new ListItem("--All--", "0"));

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
                GetEventLogs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

    }
}