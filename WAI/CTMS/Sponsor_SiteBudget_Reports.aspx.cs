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
    public partial class Sponsor_SiteBudget_Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Rows.Add(1, "Site Budget");
                dt.Rows.Add(2, "Subject Budget");
                gvreport.DataSource = dt;
                gvreport.DataBind();

                DAL dal;
                dal = new DAL();
                if (Convert.ToString(Session["PROJECTID"]) != "")
                {
                    DataSet ds1 = dal.GetSiteID(
                    Action: "INVID",
                    PROJECTID: Session["PROJECTID"].ToString(),
                    User_ID: Session["User_ID"].ToString()
                    );
                    drp_InvID.DataSource = ds1.Tables[0];
                    drp_InvID.DataValueField = "INVNAME";
                    drp_InvID.DataBind();
                    drp_InvID.Items.Insert(0, new ListItem("--ALL--", "0"));
                }
            }
        }

        protected void ddl_INVID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Subject();
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
                DataSet ds = dal.SiteBudget_SP(Action: "get_Subject", Project_Id: Session["PROJECTID"].ToString(), Site_ID: drp_InvID.SelectedValue);
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
    }
}