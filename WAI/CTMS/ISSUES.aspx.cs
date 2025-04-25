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
    public partial class ISSUES1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["PROJECTID"] != null)
                    {
                        Drp_Project.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                        fill_Inv();
                    }
                    else
                    {
                        fill_Project();
                    }
                   
                    fill_status();
                    getData();
                  

                }




            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Project()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project.DataSource = ds.Tables[0];
                Drp_Project.DataValueField = "Project_ID";
                Drp_Project.DataTextField = "PROJNAME";
                Drp_Project.DataBind();
                Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
        private void fill_Inv()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSiteID(
                Action: "INVID",
                PROJECTID: Session["PROJECTID"].ToString(),
                User_ID: Session["User_ID"].ToString()
                );
                drp_InvID.DataSource = ds.Tables[0];
                drp_InvID.DataValueField = "INVNAME";
                drp_InvID.DataBind();
                drp_InvID.Items.Insert(0, new ListItem("--ALL--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_status()
        {
            try
            {
                DAL dal = new DAL();

                DataSet ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
                dal.BindDropDown(Drp_Status, ds.Tables[0]);
               
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void getData()
        {
            try
            {

                DAL dal;
                dal = new DAL();
                if (Drp_Project.SelectedValue == "0")
                {
                    DataSet ds = new DataSet();
                    ds = dal.getsetISSUES(
                    Action: "ISSUES_LIST"
                    );
                    grdISSUES.DataSource = ds;
                    grdISSUES.DataBind();
                }
                if (Drp_Project.SelectedValue !="0" && drp_InvID.SelectedValue=="0" && Drp_Status.SelectedValue=="0")
                {
                    DataSet ds = new DataSet();
                    ds = dal.getsetISSUES(
                    Action: "ISSUES_LIST_Project",
                    Project_ID:Drp_Project.SelectedValue
                    );
                    grdISSUES.DataSource = ds;
                    grdISSUES.DataBind();
                }
                if (Drp_Project.SelectedValue != "0" && drp_InvID.SelectedValue != "0" && Drp_Status.SelectedValue == "0")
                {
                    DataSet ds = new DataSet();
                    ds = dal.getsetISSUES(
                    Action: "ISSUES_LIST_INVID",
                    Project_ID: Drp_Project.SelectedValue,
                    INVID:drp_InvID.SelectedValue
                    );
                    grdISSUES.DataSource = ds;
                    grdISSUES.DataBind();
                }

                if (Drp_Project.SelectedValue == "0" && drp_InvID.SelectedValue == "0" && Drp_Status.SelectedValue != "0")
                {
                    DataSet ds = new DataSet();
                    ds = dal.getsetISSUES(
                    Action: "ISSUES_LIST_Status",
                    Status: Drp_Status.SelectedValue
                    );
                    grdISSUES.DataSource = ds;
                    grdISSUES.DataBind();
                }
                if (Drp_Project.SelectedValue != "0" && Drp_Status.SelectedValue != "0" && drp_InvID.SelectedValue == "0")
                {
                    DataSet ds = new DataSet();
                    ds = dal.getsetISSUES(
                    Action: "ISSUES_LIST_Status_Project",
                    Project_ID: Drp_Project.SelectedValue,
                    Status: Drp_Status.SelectedValue
                    );
                    grdISSUES.DataSource = ds;
                    grdISSUES.DataBind();
                }
                if (Drp_Project.SelectedValue != "0" && drp_InvID.SelectedValue != "0" && Drp_Status.SelectedValue != "0")
                {
                    DataSet ds = new DataSet();
                    ds = dal.getsetISSUES(
                    Action: "ISSUES_LIST_Status_Project_INVID",
                    Project_ID: Drp_Project.SelectedValue,
                    INVID: drp_InvID.SelectedValue,
                     Status: Drp_Status.SelectedValue
                    );
                    grdISSUES.DataSource = ds;
                    grdISSUES.DataBind();
                }
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Drp_Project_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Inv();
                getData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drp_InvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void Drp_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdISSUES_PreRender(object sender, EventArgs e)
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

        protected void grdISSUES_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                string id = e.CommandArgument.ToString();
                dal.getsetISSUES(
                    Action: "Delete",
                    ISSUES_ID: id);
                getData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}