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
    public partial class frnCOUNTRYDETAILS : System.Web.UI.Page
    {
        DAL dal;
        DAL constr = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    fill_drpdwn();
                    fill_Country();
                    GetRecords();
                }
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
                dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                ds = dal.GetSetCOUNTRYDETAILS(
                Action: "GET_COUNTRY",
                Project_Name: Drp_Project.SelectedItem.Text,
                ENTEREDBY: Session["User_ID"].ToString()
                );
                rptCountryAdded.Visible = true;
                rptCountryAdded.DataSource = ds.Tables[0];
                rptCountryAdded.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
               // lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void addNewRecord(int rownum)
        {
            try
            {
                  CheckBox ChAction;
                  ChAction = (CheckBox)CountryMaster.Rows[rownum].FindControl("Chk_Sel_Fun");
                  if (ChAction.Checked)
                    {
                        lblErrorMsg.Text = "";
                        DAL dal;
                        dal = new DAL();
                        DataSet ds = dal.GetSetCOUNTRYDETAILS(
                        Action: "INSERT_COUNTRY_DETAILS",
                        Project_Name: Drp_Project.SelectedItem.Text,
                        CNTRYID: ((TextBox)CountryMaster.Rows[rownum].FindControl("CNTRYID")).Text,
                        ENTEREDBY: Session["User_ID"].ToString(),
                        Order_ID_Prefix: ((TextBox)CountryMaster.Rows[rownum].FindControl("txtOrderID_Prefix")).Text,
                        Order_ID_LastNo: Convert.ToInt32(((TextBox)CountryMaster.Rows[rownum].FindControl("txtOrderID_LastNo")).Text),
                        Order_IDSD_Prefix: ((TextBox)CountryMaster.Rows[rownum].FindControl("txtOrderID_SDPrefix")).Text,
                        Order_IDSD_LastNo: Convert.ToInt32(((TextBox)CountryMaster.Rows[rownum].FindControl("txtOrderID_SDLastNo")).Text)
                     );
                    }
            }
            catch (Exception ex)
            {
                throw ex;
               // lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCountryCode.Text != "")
                {
                    DAL dal;
                    dal = new DAL();
                    DataSet ds = dal.GetSetCOUNTRYDETAILS(
                    Action: "INSERT_COUNTRY_MASTER",
                    Project_Name:Drp_Project.SelectedItem.Text,
                    COUNTRYCOD: txtCountryCode.Text,
                    COUNTRY: txtCountry.Text,
                    ENTEREDBY: Session["User_ID"].ToString()
                 );
                }
                int rownum;
                for (rownum = 0; rownum < CountryMaster.Rows.Count; rownum++)
                {
                    addNewRecord(rownum);
                }
                Response.Write("<script> alert('Record Updated successfully.');window.location='frnCOUNTRYDETAILS.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void fill_Country()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "ALL_COUNTRY",
                    // Project_Name: Drp_Project.SelectedItem.Text,
                ENTEREDBY: Session["User_ID"].ToString()
                );
                //drpCountry.DataSource = ds.Tables[0];
                //drpCountry.DataValueField = "CNTRYID";
                //drpCountry.DataTextField = "COUNTRY";
                //drpCountry.DataBind();
                //drpCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
                CountryMaster.DataSource = ds;
                CountryMaster.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void fill_drpdwn()
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
                throw ex;
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //fill_Country();
                GetRecords();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
     
    }
}