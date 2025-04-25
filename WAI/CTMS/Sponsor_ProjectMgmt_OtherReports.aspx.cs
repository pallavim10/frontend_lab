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
    public partial class Sponsor_ProjectMgmt_OtherReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));                
                dt.Rows.Add(1,"Task OwnerShip matrix");
                dt.Rows.Add(2, "Project TimeLine");
                dt.Rows.Add(3, "CRA Project TimeLine");
                dt.Rows.Add(4, "Site Issue");
                dt.Rows.Add(5, "Budget");
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

                    DAL dal2 = new DAL();

                    DataSet ds2 = new DataSet();
                    ds2 = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
                    dal.BindDropDown(Drp_Status, ds2.Tables[0]);
                }
            }

        }             
    }
}