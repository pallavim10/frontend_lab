using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Net;
using System.Net.Sockets;

namespace CTMS
{
    public partial class USERLOGIN_AUTHENTICATION : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!this.IsPostBack)
                {
                    fill_drpdwn_User_ID();
                    BINDUSERASSIGNDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        public string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string ip_add = "";
            foreach (var ipp in host.AddressList)
            {
                if (ipp.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip_add = ipp.ToString();
                }
            }
            return ip_add;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string IPADDRESS = GetIpAddress();

            try
            {
                DataSet ds = dal.ManageUserGroups(ACTION: "AssignUserLogindate", User_ID: Drp_User.SelectedValue, PROJECTID: Drp_Project_Name.SelectedValue, StartDate: txtstartdate.Text, EndDate: txtenddate.Text, ENTEREDBY: Session["User_ID"].ToString(), StartTime: txtstarttime.Text, EndTime: txtendtime.Text, IPADDRESS: IPADDRESS);
                Clear();
                BINDUSERASSIGNDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_drpdwn_User_ID()
        {
            try
            {
                DataSet ds = dal.ManageUserGroups(ACTION: "GETUSERASPERPROJECT");
                Drp_User.DataSource = ds.Tables[0];
                Drp_User.DataTextField = "User_Name";
                Drp_User.DataValueField = "User_ID";
                Drp_User.DataBind();
                Drp_User.Items.Insert(0, new ListItem("--Select User--", "0"));
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void fill_Proj_Name()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.ManageUserGroups(
            ACTION: "BINDPROJECTBYUSER",
            User_ID: Drp_User.SelectedValue
            );
            Drp_Project_Name.DataSource = ds;
            Drp_Project_Name.DataTextField = "PROJNAME";
            Drp_Project_Name.DataValueField = "Project_ID";
            Drp_Project_Name.DataBind();
            Drp_Project_Name.Items.Insert(0, new ListItem("--Select Project Name--", "0"));
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Clear()
        {
            Drp_Project_Name.Enabled = true;
            Drp_User.Enabled = true;
            Drp_Project_Name.Items.Clear();
            Drp_User.SelectedIndex = 0;
            txtstartdate.Text = "";
            txtenddate.Text = "";
            txtstarttime.Text = "";
            txtendtime.Text = "";
        }

        protected void Drp_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Proj_Name();
                BINDUSERASSIGNDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BINDUSERASSIGNDATA()
        {
            try
            {
                DataSet ds = dal.ManageUserGroups(ACTION: "BINDUSERASSIGNDATA", User_ID: Drp_User.SelectedValue, PROJECTID: Drp_Project_Name.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdUserAssignData.DataSource = ds.Tables[0];
                    grdUserAssignData.DataBind();
                }
                else
                {
                    grdUserAssignData.DataSource = null;
                    grdUserAssignData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BINDUSERASSIGNDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserAssignData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = Convert.ToString(e.CommandArgument);
                if (e.CommandName == "EditData")
                {
                    EDITUSERDATA(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void EDITUSERDATA(string ID)
        {
            try
            {
                Session["ASSIGNUSERID"] = ID;
                DataSet ds = dal.ManageUserGroups(ACTION: "BINDUSERASSIGNDATABYID", ID: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {                    
                    Drp_User.SelectedValue = ds.Tables[0].Rows[0]["User_ID"].ToString();
                    fill_Proj_Name();
                    Drp_Project_Name.SelectedValue = ds.Tables[0].Rows[0]["PROJECTID"].ToString();
                    txtstartdate.Text = ds.Tables[0].Rows[0]["STARTDATE"].ToString();
                    txtstarttime.Text = ds.Tables[0].Rows[0]["STARTTIME"].ToString();
                    txtenddate.Text = ds.Tables[0].Rows[0]["ENDDATE"].ToString();
                    txtendtime.Text = ds.Tables[0].Rows[0]["ENDTIME"].ToString();
                    Drp_Project_Name.Enabled = false;
                    Drp_User.Enabled = false;
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string IPADDRESS = GetIpAddress();
            try
            {
                DataSet ds = dal.ManageUserGroups(ACTION: "UpdateAssignUserLogindate", StartDate: txtstartdate.Text, EndDate: txtenddate.Text, ENTEREDBY: Session["User_ID"].ToString(), StartTime: txtstarttime.Text, EndTime: txtendtime.Text, ID: Session["ASSIGNUSERID"].ToString(), IPADDRESS: IPADDRESS);
                Clear();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                BINDUSERASSIGNDATA();
                Drp_Project_Name.Enabled = true;
                Drp_User.Enabled = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserAssignData_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
                {
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {
                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void lbAssignLogDtExport_Click(object sender, EventArgs e)
        {
            try
            {
                Get_Assign_User_Login(header: "Assign User Login Date", Action: "Get_Assign_User_Login");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void Get_Assign_User_Login(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.Export_Data_SP(Action: Action, PRODUCTID: PRODUCT_ID, SUBCLASSID: SUBCLASS_ID);

                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



    }
}