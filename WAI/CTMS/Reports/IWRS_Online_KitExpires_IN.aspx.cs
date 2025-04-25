using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CTMS
{
    public partial class IWRS_Online_KitExpires_IN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["PROJECTID"] != null)
                    {
                        Drp_Project_Name.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTIDTEXT"].ToString()));
                        fill_drpdwn_Site_ID();
                    }
                    else
                    {
                        fill_Proj_Name();
                    }
                }
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void fill_drpdwn_Site_ID()
        {
            try
            {

                //ServiceIWRSClient proxy = new ServiceIWRSClient();
                //IWRSData obj = new IWRSData()
                //{
                //    Action = "INVID",
                //    Project_Name = Drp_Project_Name.SelectedValue,

                //    UserID = Session["User_ID"].ToString()
                //};
                //DataSet ds = proxy.GetSiteID(obj);
                //proxy.Close();

                //Drp_Site_ID.DataSource = ds.Tables[0];
                //Drp_Site_ID.DataValueField = "INVID";
                //Drp_Site_ID.DataBind();

            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void fill_Proj_Name()
        {
            try
            {

                //ServiceIWRSClient proxy = new ServiceIWRSClient();
                //IWRSData obj = new IWRSData()
                //{
                //    Action = "Get_Specific_Project",
                //    ProjectID = Session["PROJECTID"].ToString(),
                //    UserID = Session["User_ID"].ToString()
                //};
                //DataSet ds = proxy.GetSetPROJECTDETAILS(obj);
                //proxy.Close();

                //Drp_Project_Name.DataSource = ds.Tables[0];
                //Drp_Project_Name.DataValueField = "Project_Name";
                //Drp_Project_Name.DataBind();
                //Drp_Project_Name.Items.Insert(0, new ListItem("--Select Project--", "0"));


            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetData()
        {
            try
            {
                lblErrorMsg.Text = "";

                //ServiceIWRSClient proxy = new ServiceIWRSClient();
                //IWRSData obj = new IWRSData()
                //{
                //    Action = "Kit_Expires_IN",
                //    Project_Name = Drp_Project_Name.SelectedValue.ToString(),
                //    INVID = Drp_Site_ID.SelectedValue.ToString(),
                //    Days=Drp_Days.SelectedValue.ToString()
                //};
                //DataSet ds = proxy.IWRS_Report_Online(obj);
                //proxy.Close();
                //grd_data.DataSource = ds.Tables[0];
                //grd_data.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Drp_Site_ID_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void grd_data_PreRender(object sender, EventArgs e)
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

            }
        }

        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_drpdwn_Site_ID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Drp_Days_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}