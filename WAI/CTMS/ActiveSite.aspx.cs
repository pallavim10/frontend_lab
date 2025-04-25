using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Data.SqlClient;

namespace CTMS
{

    public partial class ActiveSite : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL constr = new DAL();
        CommonFunction.CommonFunction CF = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    fill_drpdwn_Site_ID();
                }

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
                DataSet ds = dal.GetSiteID(Action: "INVID", PROJECTID: Session["PROJECTID"].ToString(), User_ID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        Drp_Site_ID.DataSource = ds.Tables[0];
                        Drp_Site_ID.DataValueField = "INVNAME";
                        Drp_Site_ID.DataBind();
                    }
                    else
                    {
                        Drp_Site_ID.DataSource = ds.Tables[0];
                        Drp_Site_ID.DataValueField = "INVNAME";
                        Drp_Site_ID.DataBind();
                        Drp_Site_ID.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
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

        protected void Btn_SendMail_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp;
                SqlConnection con = new SqlConnection(constr.getconstr());
                string EAddress, ECcAdd, ESubject, EBody;

                EAddress = "";
                ECcAdd = "";

                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Connection = con;
                con.Open();
                cmd1.CommandText = "GET_PROJECT_EMAILS";
                cmd1.Parameters.AddWithValue("@Project_ID", Session["PROJECTID"].ToString());
                cmd1.Parameters.AddWithValue("@Email_Type", "SiteAct");
                cmd1.Parameters.AddWithValue("@INVID", Drp_Site_ID.SelectedValue);
                adp = new SqlDataAdapter(cmd1);
                adp.Fill(ds);
                cmd1.Dispose();
                EAddress = ds.Tables[0].Rows[0]["E_CC"].ToString();
                con.Close();



                SqlDataReader myReader2;
                SqlCommand cmd5 = new SqlCommand();
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.Connection = con;
                con.Open();
                cmd5.CommandText = "GET_PROJECT_EMAILS";
                cmd5.Parameters.AddWithValue("@Project_ID", Session["PROJECTID"].ToString());
                cmd5.Parameters.AddWithValue("@Email_Type", "SiteAct");
                myReader2 = cmd5.ExecuteReader();
                while (myReader2.Read())
                {
                    ECcAdd = myReader2["E_CC"].ToString();
                }
                con.Close();

                EBody = "Site Id : " + Drp_Site_ID.SelectedValue + " is activated for Protocol ID:  " + Session["PROJECTIDTEXT"].ToString() + " .";
                ESubject = "Protocol ID-" + Session["PROJECTIDTEXT"].ToString() + " Site Activation";
                CF.Email_Users(EAddress, ECcAdd, ESubject, EBody);

                Response.Write("<script> alert('Site Activated Successfully.');window.location='ActiveSite.aspx'; </script>");
            }
            catch (Exception ex) 
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;

            }
        }

    }
}