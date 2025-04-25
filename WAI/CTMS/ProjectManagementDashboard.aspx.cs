using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class ProjectManagementDashboard : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Get_Dashboard();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Dashboard()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_DASHBOARD_PM");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblReportFinalTime.Text = ds.Tables[0].Rows[0]["ReportFinalTime"].ToString();
                    lblReportUnderReview.Text = ds.Tables[0].Rows[0]["ReportUnderReview"].ToString();
                    lblReportFinalised.Text = ds.Tables[0].Rows[0]["ReportFinalised"].ToString();
                }
                else
                {
                    lblReportFinalTime.Text = "0";
                    lblReportUnderReview.Text = "0";
                    lblReportFinalised.Text = "0";
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatDashboard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string uName = row["Usercontrol_Name"].ToString();
                    string X = row["X"].ToString();
                    string Y = row["Y"].ToString();
                    string Width = row["Width"].ToString();
                    string Height = row["Height"].ToString();

                    HtmlGenericControl divMain = e.Item.FindControl("divMain") as HtmlGenericControl;
                    if (X != null && X.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-x");
                        divMain.Attributes.Add("data-gs-x", X);
                    }
                    if (Y != null && Y.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-y");
                        divMain.Attributes.Add("data-gs-y", Y);
                    }
                    if (Width != null && Width.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-width");
                        divMain.Attributes.Add("data-gs-width", Width);
                    }
                    if (Height != null && Height.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-height");
                        divMain.Attributes.Add("data-gs-height", Height);
                    }

                    string usercontrolName = "~/Dashboard Charts/" + uName;
                    UserControl uc = (UserControl)Page.LoadControl(usercontrolName);
                    PlaceHolder placeHolder = e.Item.FindControl("placeHolder") as PlaceHolder;
                    placeHolder.Controls.Add(uc);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}