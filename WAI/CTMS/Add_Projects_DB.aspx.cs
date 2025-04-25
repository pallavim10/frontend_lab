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
    public partial class Add_Projects_DB : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    BINDPROJECTDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BINDPROJECTDATA()
        {
            try
            {
                DataSet ds = dal.ManageUserGroups(ACTION: "CHECKDBCREATEDORNOT");
                grdProjectDB.DataSource = ds;
                grdProjectDB.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdProjectDB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string DBCREATED = dr["DBCREATED"].ToString();
                    CheckBox CHKDB = (CheckBox)e.Row.FindControl("CHKDB");

                    if (DBCREATED == "CREATED")
                    {
                        CHKDB.Visible = false;
                    }
                    else
                    {
                        CHKDB.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < grdProjectDB.Rows.Count; j++)
                {
                    string PROJECTID = ((Label)grdProjectDB.Rows[j].FindControl("lblProjectID")).Text;
                    string PROJECTNAME = ((Label)grdProjectDB.Rows[j].FindControl("lblPROJNAME")).Text;
                    CheckBox chkdb = (CheckBox)grdProjectDB.Rows[j].FindControl("CHKDB");
                    if (chkdb.Checked == true)
                    {
                        string con = dal.getconstr();
                        string[] parts = con.Split(';');

                        string dataSource = "", InitialCatalog = "", IntegratedSecurity = "", UserID = "", Password = "";
                        for (int i = 0; i < parts.Length; i++)
                        {
                            string part = parts[i].Trim();
                            if (part.StartsWith("Data Source="))
                            {
                                dataSource = part.Replace("Data Source=", "");

                            }
                            if (part.StartsWith("Initial Catalog="))
                            {
                                InitialCatalog = part.Replace("Initial Catalog=", "");

                            }
                            if (part.StartsWith("Integrated Security="))
                            {
                                IntegratedSecurity = part.Replace("Integrated Security=", "");

                            }
                            if (part.StartsWith("User ID="))
                            {
                                UserID = part.Replace("User ID=", "");

                            }
                            if (part.StartsWith("Password="))
                            {
                                Password = part.Replace("Password=", "");

                            }
                        }

                        string ConnString = "Data Source=" + dataSource + ";" + "Initial Catalog=" + InitialCatalog + "_" + Convert.ToInt32(PROJECTID) + ";" + "Integrated Security=" + IntegratedSecurity + ";" + "User ID=" + UserID + ";" + "Password=" + Password;
                        DataTable dt = dal.CREATECHILDDB(ACTION: "checkdbname", CHILDDBNAME: InitialCatalog + "_" + Convert.ToInt32(PROJECTID));
                        if (dt.Rows.Count > 0)
                        {
                            Response.Write("<script> alert('Database is Already Exists.')</script>");
                        }
                        else
                        {
                            dal.CREATECHILDDB(ACTION: "CREATE CHILD DB", CHILDDBNAME: InitialCatalog + "_" + Convert.ToInt32(PROJECTID));

                            bool Success = false;
                            do
                            {
                                Success = dal.CREATECHILDDBSCRIPT("CREATECHILDDBSCRIPT.sql", ConnString);
                            }
                            while (Success == false);

                            dal.Defaultscript("Defaultscript.sql", ConnString);

                            dal.GetSetPROJECTDETAILS
                                (
                                Action: "UpdateDBName",
                                Project_ID: Convert.ToInt32(PROJECTID),
                                ConnString: ConnString,
                                ChildDBName: InitialCatalog + "_" + Convert.ToInt32(PROJECTID),
                                ENTEREDBY: Session["User_ID"].ToString()
                                );


                            string MESSAGE = "DataBase " + InitialCatalog + "_" + Convert.ToInt32(PROJECTID) + " of " + PROJECTNAME + " Created SuccessFully";

                            //SendEmail(PROJECTID, PROJECTNAME, InitialCatalog + "_" + Convert.ToInt32(PROJECTID));

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='Add_Projects_DB.aspx';", true);
                        }
                    }
                }
                //BINDPROJECTDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SendEmail(string PROEJECTID, string PROJECTNAME, string DBNAME)
        {
            try
            {
                string EmailAdd = "helpdesk@diagnosearch.com";
                string CCEmailAddress = "";
                string E_Sub = "";
                string E_Body = "";
                CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

                DataSet ds = dal.AddUserProfile(Action: "GETUSERNAME", USERID: Session["User_ID"].ToString());
                string USERNAME = ds.Tables[0].Rows[0]["User_Name"].ToString();

                E_Sub = "New DataBase Created";
                E_Body = "<html><head><title>New DB Created</title></head><body><h2>Hi Anish,</h2><p>New Database is Created by " + @USERNAME + "</p><p>ProjectID is " + @PROEJECTID + "</p><p>Project Name is " + @PROJECTNAME + "</p><p>DataBase Name is " + @DBNAME + "</p></body></html>";
                //E_Body = "Hii,\n \n Your ProjectID is " + @PROEJECTID + ",\n Project Name is " + @PROJECTNAME + ", \n DataBase Name is " + @DBNAME + "";
                EmailAdd = "anish.raut@diagnosearch.com";

                commFun.Email_Users(EmailAdd, CCEmailAddress, E_Sub, E_Body);
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