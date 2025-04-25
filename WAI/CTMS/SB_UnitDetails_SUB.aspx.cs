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
    public partial class SB_UnitDetails_SUB : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Bind_InvID();
                    btnsubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_GV()
        {
            try
            {
                if (ddl_Site.SelectedIndex != 0 && ddl_Subject.SelectedIndex != 0)
                {
                    DataSet ds = dal.SiteBudget_SP(Action: "get_Visit", Project_Id: Session["PROJECTID"].ToString());
                    gvVisits.DataSource = ds.Tables[0];
                    gvVisits.DataBind();
                }
                else
                {
                    gvVisits.DataSource = null;
                    gvVisits.DataBind();
                }

                if (gvVisits.Rows.Count > 0)
                {
                    btnsubmit.Visible = true;
                }
                else
                {
                    btnsubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Bind_InvID()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSiteID(
                Action: "INVID",
                PROJECTID: Session["PROJECTID"].ToString(), User_ID: Session["User_ID"].ToString()
                );
                ddl_Site.DataSource = ds.Tables[0];
                ddl_Site.DataValueField = "INVNAME";
                ddl_Site.DataBind();
                ddl_Site.Items.Insert(0, new ListItem("--Select--", "99"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Bind_Subject()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.SiteBudget_SP(Action: "get_Subject", Project_Id: Session["PROJECTID"].ToString(), Site_ID: ddl_Site.SelectedValue);
                ddl_Subject.DataSource = ds.Tables[0];
                ddl_Subject.DataValueField = "SUBJID";
                ddl_Subject.DataBind();
                ddl_Subject.Items.Insert(0, new ListItem("--Select--", "99"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_INVID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Site.SelectedIndex != 0)
                {
                    Bind_Subject();
                }
                else
                {
                    btnsubmit.Visible = false;
                    gvVisits.DataSource = null;
                    gvVisits.DataBind();
                    Bind_Subject();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_Subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Subject.SelectedIndex != 0 && ddl_Site.SelectedIndex != 0)
                {
                    btnsubmit.Visible = true;
                    bind_GV();
                }
                else
                {
                    btnsubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvRates_PreRender(object sender, EventArgs e)
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

        protected void gvVisits_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();

                    Session["VISIT_ID"] = ID;
                    GridView gvRates = e.Row.FindControl("gvRates") as GridView;
                    DataSet ds = dal.SiteBudget_SP(Action: "get_Visit_Task_Units_Sub", Project_Id: Session["PROJECTID"].ToString(), Site_ID: ddl_Site.SelectedValue, SUBJID: ddl_Subject.SelectedValue, Visit_Id: ID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvRates.DataSource = ds.Tables[0];
                        gvRates.DataBind();
                    }
                    else
                    {
                        gvRates.DataSource = null;
                        gvRates.DataBind();
                    }

                    Label lbl_Amt = (e.Row.FindControl("lbl_Amt") as Label);
                    int Amt = 0;
                    foreach (GridViewRow row in gvRates.Rows)
                    {
                        Label lblAmount = (row.FindControl("lblAmount") as Label);
                        if (lblAmount.Text != "")
                        {
                            Amt += Convert.ToInt32(lblAmount.Text);
                        }
                    }
                    lbl_Amt.Text = Amt.ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void gvRates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string Type = dr["Type"].ToString();
                    string Units = dr["Units"].ToString();
                    string Visit_ID = dr["Visit_Id"].ToString();
                    string Visit = dr["Visit"].ToString();

                    DropDownList ddlType = e.Row.FindControl("ddlType") as DropDownList;
                    ddlType.Items.Clear();
                    ddlType.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlType.Items.Insert(1, new ListItem("Fixed", "Fixed"));
                    ddlType.Items.Insert(2, new ListItem("Variable", "Variable"));

                    if (Type != "")
                    {
                        ddlType.SelectedValue = Type;
                    }
                    else
                    {
                        ddlType.SelectedIndex = 0;
                    }

                    DropDownList ddlShift = e.Row.FindControl("ddlShift") as DropDownList;
                    if (Units == "" || Units == "0")
                    {
                        DataSet ds1 = dal.SiteBudget_SP(Action: "get_Visit", Project_Id: Session["PROJECTID"].ToString());
                        ddlShift.DataSource = ds1.Tables[0];
                        ddlShift.DataValueField = "ID";
                        ddlShift.DataTextField = "Visit";
                        ddlShift.DataBind();
                        ddlShift.Items.Insert(0, new ListItem("--Select--", "99"));
                        ddlShift.Visible = true;
                    }
                    else
                    {
                        ddlShift.Visible = false;
                    }

                    Label lbl_TaskName = e.Row.FindControl("lbl_TaskName") as Label;
                    if (Visit_ID != Session["Visit_Id"].ToString())
                    {
                        lbl_TaskName.ForeColor = System.Drawing.Color.Red;
                        lbl_TaskName.ToolTip = Visit;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                insert_units();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_units()
        {
            try
            {
                for (int c = 0; c < gvVisits.Rows.Count; c++)
                {
                    GridView gvRates = gvVisits.Rows[c].FindControl("gvRates") as GridView;
                    string lbl_VisitID = ((Label)gvVisits.Rows[c].FindControl("lbl_VisitID")).Text;

                    for (int i = 0; i < gvRates.Rows.Count; i++)
                    {
                        string Task_Id = ((Label)gvRates.Rows[i].FindControl("lbl_TaskId")).Text;
                        string Sub_Task_Id = ((Label)gvRates.Rows[i].FindControl("lbl_SubTaskId")).Text;
                        string ddlType = ((DropDownList)gvRates.Rows[i].FindControl("ddlType")).SelectedItem.Text;
                        string lblRate = ((Label)gvRates.Rows[i].FindControl("lblRate")).Text;
                        string txtUnits = ((TextBox)gvRates.Rows[i].FindControl("txtUnits")).Text;
                        string lbl_ActID = ((Label)gvRates.Rows[i].FindControl("lbl_ActID")).Text;

                        if (ddlType == "--Select--")
                        {
                            ddlType = string.Empty;
                        }

                        if (txtUnits.ToString() != "")
                        {
                            int lblAmount = Convert.ToInt32(lblRate) * Convert.ToInt32(txtUnits);
                            dal.SiteBudget_SP(Action: "insert_Units_Sub", Task_Id: Task_Id, Sub_Task_ID: Sub_Task_Id, Type: ddlType, Rate: lblRate, Units: txtUnits, Amount: lblAmount.ToString(), Project_Id: Session["PROJECTID"].ToString(), Site_ID: ddl_Site.SelectedItem.Text, Visit_Id: lbl_VisitID, SUBJID: ddl_Subject.SelectedValue);
                        }
                        else
                        {
                            DropDownList ddlShift = (gvRates.Rows[i].FindControl("ddlShift") as DropDownList);
                            if (ddlShift.SelectedIndex != 0)
                            {
                                dal.SiteBudget_SP(Action: "shift_Act", Site_ID: ddl_Site.SelectedValue, SUBJID: ddl_Subject.SelectedValue, Act_Id: lbl_ActID, Id: ddlShift.SelectedValue, Visit_Id: lbl_VisitID);
                            }
                        }
                    }
                }
                bind_GV();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}