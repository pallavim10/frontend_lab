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
    public partial class CTMS_MonitoringMatrix : System.Web.UI.Page
    {
        DAL constr = new DAL();
        CommonFunction.CommonFunction CF = new CommonFunction.CommonFunction();
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
                        fill_VisitType();
                        GetRecords();
                    }
                    else
                    {
                        fill_Project();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void fill_VisitType()
        {
            try
            {                
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.drp_SectionsMaster();
                drpVisitType.DataSource = ds.Tables[0];
                drpVisitType.DataValueField = "SECTIONID";
                drpVisitType.DataTextField = "MODULENAME";
                drpVisitType.DataBind();
                drpVisitType.Items.Insert(0, new ListItem("--All--", "99"));
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
                if (Session["PROJECTID"] != null)
                {
                    Drp_Project.Items.FindByValue(Session["PROJECTID"].ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                PROJECTID: Session["PROJECTID"].ToString(), User_ID: Session["User_ID"].ToString()
                );
                drp_INVID.DataSource = ds.Tables[0];
                drp_INVID.DataValueField = "INVNAME";
                drp_INVID.DataBind();
                drp_INVID.Items.Insert(0, new ListItem("--All--", "99"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetRecords()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetMonitoringVisit(
                Action: "GET_MONITORING_MATRIX",
                PROJECTID: Drp_Project.SelectedItem.Value,
                INVID: drp_INVID.SelectedItem.Value,
                VISITTYPE: drpVisitType.SelectedItem.Value
                );
                grd.DataSource = ds;
                grd.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void drpVisitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRecords();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void Drp_Project_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Inv();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drp_INVID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRecords();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
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
    }
}