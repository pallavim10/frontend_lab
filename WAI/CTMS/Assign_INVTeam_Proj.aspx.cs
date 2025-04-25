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
    public partial class Assign_INVTeam_Proj : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    fill_PROJECT();
                    GETINVNAME();
                    GETRECORDS();

                    //GETADDEDINVID();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_PROJECT()
        {
            try
            {
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project.DataSource = ds.Tables[0];
                Drp_Project.DataValueField = "Project_ID";
                Drp_Project.DataTextField = "PROJNAME";
                Drp_Project.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
                    Drp_Project.SelectedValue = ds.Tables[0].Rows[0]["Project_ID"].ToString();
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
                }



            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GETINVNAME()
        {
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "GET_INVBY_PROJ", Project_ID: Drp_Project.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlINVNAME.DataSource = ds;
                    ddlINVNAME.DataTextField = "INVNAM";
                    ddlINVNAME.DataValueField = "INVCOD";
                    ddlINVNAME.DataBind();
                    ddlINVNAME.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlINVNAME.Items.Clear();
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
                GETRECORDS();
                GETINVNAME();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlINVNAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETRECORDS();
                GETINVTEAMNAME();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GETRECORDS()
        {
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "GET_INVTEAM_ASSIGN_DATA",
                Project_ID: Drp_Project.SelectedValue,
                INVCOD: ddlINVNAME.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdInvTeam.DataSource = ds;
                    grdInvTeam.DataBind();
                }
                else
                {
                    grdInvTeam.DataSource = null;
                    grdInvTeam.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GETINVTEAMNAME()
        {
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "GET_INVTEAM_MEMBERBY_INVID", Project_ID: Drp_Project.SelectedValue, INVCOD: ddlINVNAME.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpInvTeamMem.DataSource = ds;
                    drpInvTeamMem.DataTextField = "NAME";
                    drpInvTeamMem.DataValueField = "ID";
                    drpInvTeamMem.DataBind();
                    drpInvTeamMem.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpInvTeamMem.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "INSERT_INVTEAM_PROJ",
                Project_ID: Drp_Project.SelectedValue,
                INVCOD: ddlINVNAME.SelectedValue,
                INVTEAMID: drpInvTeamMem.SelectedValue,
                ENTEREDBY: Session["User_ID"].ToString(),
                STARTDATE: txtstartdate.Text,
                ENDDATE: txtenddate.Text
                );
                CLEAR();
                GETRECORDS();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "UPDATE_INVTEAM_PROJ",
                   ID: Session["ASSIGN_INVTEAM_ID"].ToString(),
                   STARTDATE: txtstartdate.Text,
                   ENDDATE: txtenddate.Text,
                   ENTEREDBY: Session["User_ID"].ToString()
                   );
                ddlINVNAME.Enabled = true;
                drpInvTeamMem.Enabled = true;
                Drp_Project.Enabled = true;
                CLEAR();
                GETRECORDS();

                btnUpdate.Visible = false;
                bntSave.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdInvAdded_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = Convert.ToString(e.CommandArgument);
                if (e.CommandName == "EDITINV")
                {
                    EDITINV(ID);
                }
                else if (e.CommandName == "DELETEINV")
                {
                    DELETEINV(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
      
        public void EDITINV(string ID)
        {
            try
            {
                Session["ASSIGN_INVTEAM_ID"] = ID;
                DataSet ds = dal.GetSetINVDETAILS(Action: "GET_INVTEAMBY_ID", ID: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Drp_Project.SelectedValue = ds.Tables[0].Rows[0]["PROJECTID"].ToString();
                    Drp_Project.Enabled = false;
                    Drp_Project.CssClass = "form-control required  width200px";
                    ddlINVNAME.SelectedValue = ds.Tables[0].Rows[0]["INVCODE"].ToString();
                    ddlINVNAME.Enabled = false;
                    ddlINVNAME.CssClass = "form-control required  width200px";
                    GETINVTEAMNAME();
                    drpInvTeamMem.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["INV_TEAMID"].ToString());
                    drpInvTeamMem.Enabled = false;
                    drpInvTeamMem.CssClass = "form-control required  width200px";
                    txtstartdate.Text = ds.Tables[0].Rows[0]["STARTDAT"].ToString();
                    txtenddate.Text = ds.Tables[0].Rows[0]["ENDDATE"].ToString();
                    btnUpdate.Visible = true;
                    bntSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETEINV(string ID)
        {
            
            dal = new DAL();
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "DELETE_INVTEAM_PROJ", ID: ID, ENTEREDBY: Session["User_ID"].ToString());
                GETRECORDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void CLEAR()
        {
            ddlINVNAME.SelectedIndex = 0;
            drpInvTeamMem.Items.Clear();
            txtstartdate.Text = "";
            txtenddate.Text = "";
        }

        protected void grdInvAdded_PreRender(object sender, EventArgs e)
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
       
        protected void lbAssignInvMemberEport_Click(object sender, EventArgs e)
        {
            try
            {
                Assign_INV_TeamMember(header: "Assign Project Investigator Team Member", Action: "Assign_Project_INV_TeamMember");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Assign_INV_TeamMember(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
        {
            try
            {
                DataSet ds = new DataSet();
                DAL dal;
                dal = new DAL();
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