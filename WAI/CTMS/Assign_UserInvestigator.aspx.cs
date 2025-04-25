using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;

namespace CTMS
{
    public partial class Assign_UserInvestigator : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    fill_Proj_Name();
                    btnSubmitINVID.Visible = false;
                    btnCancelINVID.Visible = false;
                    fill_drpdwn_User_ID();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Proj_Name()
        {

            try
            {
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project_Name.DataSource = ds;
                Drp_Project_Name.DataTextField = "PROJNAME";
                Drp_Project_Name.DataValueField = "Project_ID";
                Drp_Project_Name.DataBind();
               
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Drp_Project_Name.Items.Insert(0, new ListItem("--Select Project--", "0"));
                    Drp_Project_Name.SelectedValue = ds.Tables[0].Rows[0]["Project_ID"].ToString();
    
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    Drp_Project_Name.Items.Insert(0, new ListItem("--Select Project--", "0"));
                }

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
                DataSet ds = dal.ManageUserGroups(ACTION: "GETUSERASPERPROJECT", PROJECTID: Drp_Project_Name.SelectedValue);
                Drp_User.DataSource = ds.Tables[1];
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

        private void fill_drpdwn_Site_ID()
        {
            try
            {
                if (Drp_User.SelectedValue != "0")
                {
                    SqlConnection con = new SqlConnection();
                    con = new SqlConnection(dal.getconstr());
                    SqlCommand cmd = new SqlCommand("Add_User_Profile", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INVIDBYPROJECT");
                    cmd.Parameters.AddWithValue("@PROJECTID", Drp_Project_Name.SelectedValue);
                    cmd.Parameters.AddWithValue("@USERID", Drp_User.SelectedValue);
                    cmd.Parameters.AddWithValue("@User_Name", Drp_User.SelectedItem.Text);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdINVID.DataSource = ds;
                        grdINVID.DataBind();
                        btnSubmitINVID.Visible = true;
                        btnCancelINVID.Visible = true;
                    }
                    else
                    {
                        grdINVID.DataSource = null;
                        grdINVID.DataBind();
                        btnSubmitINVID.Visible = false;
                        btnCancelINVID.Visible = false;
                    }
                }
                else
                {
                    grdINVID.DataSource = null;
                    grdINVID.DataBind();
                    btnSubmitINVID.Visible = false;
                    btnCancelINVID.Visible = false;
                }

            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;
            }
        }

        private void fill_ADDED_INVID()
        {
            try
            {
                if (Drp_User.SelectedValue != "0")
                {
                    SqlConnection con = new SqlConnection();
                    con = new SqlConnection(dal.getconstr());
                    SqlCommand cmd = new SqlCommand("Add_User_Profile", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "ASSIGNINVIDTUSER");
                    cmd.Parameters.AddWithValue("@PROJECTID", Drp_Project_Name.SelectedValue);
                    cmd.Parameters.AddWithValue("@USERID", Drp_User.SelectedValue);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdAddedINVID.DataSource = ds;
                        grdAddedINVID.DataBind();
                    }
                    else
                    {
                        grdAddedINVID.DataSource = null;
                        grdAddedINVID.DataBind();
                    }
                }
                else
                {
                    grdAddedINVID.DataSource = null;
                    grdAddedINVID.DataBind();
                    btnSubmitINVID.Visible = false;
                    btnCancelINVID.Visible = false;
                }

            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;
            }
        }

        protected void lbtndeleteSection_Click(object sender, EventArgs e)
        {
            try
            {
                string IPADDRESS = GetIpAddress();

                //DataSet ds1 = dal.AddUserProfile(Action: "CheckDBName", PROJECTID: Drp_Project_Name.SelectedValue);
                //string CON = ds1.Tables[0].Rows[0]["ConnectionString"].ToString();
                //string[] parts = CON.Split(';');
                //string CHILDDBNAME = "";
                //for (int i = 0; i < parts.Length; i++)
                //{
                //    string part = parts[i].Trim();

                //    if (part.StartsWith("Initial Catalog="))
                //    {
                //        CHILDDBNAME = part.Replace("Initial Catalog=", "");

                //    }
                //}

                for (int i = 0; i < grdAddedINVID.Rows.Count; i++)
                {
                    CheckBox chkAddedINVID = (CheckBox)grdAddedINVID.Rows[i].FindControl("chkAddedINVID");
                    if (chkAddedINVID.Checked == true)
                    {
                        string INVID = ((Label)grdAddedINVID.Rows[i].FindControl("INVID")).Text;
                        string User_ID = ((Label)grdAddedINVID.Rows[i].FindControl("User_ID")).Text;
                        string Project_ID = ((Label)grdAddedINVID.Rows[i].FindControl("Project_ID")).Text;

                        SqlConnection con = new SqlConnection(dal.getconstr());
                        SqlCommand cmd = new SqlCommand("Add_User_Profile");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@Action", "DELETE_INV");
                        cmd.Parameters.AddWithValue("@USERID", User_ID);
                        cmd.Parameters.AddWithValue("@PROJECTID", Project_ID);
                        cmd.Parameters.AddWithValue("@INVID", INVID);
                        cmd.Parameters.AddWithValue("@EnteredBy", Session["User_ID"].ToString());
                        cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);

                        cmd.ExecuteNonQuery();
                        con.Close();

                        //if (CHILDDBNAME != "")
                        //{
                        //    cmd = new SqlCommand("Add_User_Profile");
                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    cmd.Connection = con;
                        //    con.Open();
                        //    cmd.Parameters.AddWithValue("@Action", "DELETE_INVCHILD");
                        //    cmd.Parameters.AddWithValue("@USERID", User_ID);
                        //    cmd.Parameters.AddWithValue("@PROJECTID", Project_ID);
                        //    cmd.Parameters.AddWithValue("@INVID", INVID);
                        //    cmd.Parameters.AddWithValue("@CHILDDBNAME", CHILDDBNAME);
                        //    cmd.Parameters.AddWithValue("@EnteredBy", Session["User_ID"].ToString());
                        //    cmd.ExecuteNonQuery();
                        //    con.Close();
                        //}
                    }
                }
                fill_drpdwn_Site_ID();
                fill_ADDED_INVID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitINVID_Click(object sender, EventArgs e)
        {
            try
            {
                string IPADDRESS = GetIpAddress();
                //DataSet ds1 = dal.AddUserProfile(Action: "CheckDBName", PROJECTID: Drp_Project_Name.SelectedValue);
                //string CON = ds1.Tables[0].Rows[0]["ConnectionString"].ToString();
                //string[] parts = CON.Split(';');
                //string CHILDDBNAME = "";
                //for (int i = 0; i < parts.Length; i++)
                //{
                //    string part = parts[i].Trim();

                //    if (part.StartsWith("Initial Catalog="))
                //    {
                //        CHILDDBNAME = part.Replace("Initial Catalog=", "");

                //    }
                //}

                for (int i = 0; i < grdINVID.Rows.Count; i++)
                {
                    CheckBox chkINVID = (CheckBox)grdINVID.Rows[i].FindControl("chkINVID");
                    if (chkINVID.Checked == true)
                    {
                        string INVID = ((Label)grdINVID.Rows[i].FindControl("INVID")).Text;
                        SqlCommand cmd = new SqlCommand();
                        SqlConnection con = new SqlConnection();
                        con = new SqlConnection(dal.getconstr());
                        cmd = new SqlCommand("Add_User_Profile");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@Action", "INSERT_UPDATE_INV");
                        cmd.Parameters.AddWithValue("@USERID", Drp_User.SelectedValue);
                        cmd.Parameters.AddWithValue("@PROJECTID", Drp_Project_Name.SelectedValue);
                        cmd.Parameters.AddWithValue("@Site_ID", INVID);
                        cmd.Parameters.AddWithValue("@EnteredBy", Session["User_ID"].ToString());
                        cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);

                        cmd.ExecuteNonQuery();
                        con.Close();

                        //if (CHILDDBNAME != "")
                        //{
                        //    cmd = new SqlCommand("Add_User_Profile");
                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    cmd.Connection = con;
                        //    con.Open();
                        //    cmd.Parameters.AddWithValue("@Action", "INSERT_UPDATE_INVCHILD");
                        //    cmd.Parameters.AddWithValue("@USERID", Drp_User.SelectedValue);
                        //    cmd.Parameters.AddWithValue("@PROJECTID", Drp_Project_Name.SelectedValue);
                        //    cmd.Parameters.AddWithValue("@Site_ID", INVID);
                        //    cmd.Parameters.AddWithValue("@CHILDDBNAME", CHILDDBNAME);
                        //    cmd.Parameters.AddWithValue("@EnteredBy", Session["User_ID"].ToString());
                        //    cmd.ExecuteNonQuery();
                        //    con.Close();
                        //}
                    }
                }
                fill_drpdwn_Site_ID();
                fill_ADDED_INVID();
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

        protected void btnCancelINVID_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_drpdwn_User_ID();
                fill_drpdwn_Site_ID();
                fill_ADDED_INVID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Drp_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Drp_User.SelectedValue != "0")
                {
                    fill_drpdwn_Site_ID();
                    fill_ADDED_INVID();
                }
                else
                {
                    grdINVID.DataSource = null;
                    grdINVID.DataBind();
                    btnSubmitINVID.Visible = false;
                    btnCancelINVID.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

      
        protected void lbAssignINVUserExport_Click(object sender, EventArgs e)
        {
            try
            {
                Get_Assign_INV_User(header: "Assign Investigator To User", Action: "Get_Assign_INVUser");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void Get_Assign_INV_User(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
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