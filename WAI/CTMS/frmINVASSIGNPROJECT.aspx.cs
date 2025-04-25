using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Net.Sockets;
using System.Net;

namespace CTMS
{
    public partial class frmINVASSIGNPROJECT : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction Comfun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    fill_PROJECT();
                    GETINSTITUTE();
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
        private void GETINSTITUTE()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetINSTITUTEDETAILS(
                Action: "GET_INSTITUTE",
                ENTEREDBY: Session["User_ID"].ToString()
                );
                drpInstitute.DataSource = ds.Tables[0];
                drpInstitute.DataValueField = "INSTID";
                drpInstitute.DataTextField = "INSTNAM";
                drpInstitute.DataBind();
                drpInstitute.Items.Insert(0, new ListItem("--Select Institute--", "0"));
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
                GETINSTITUTE();
                GETINVNAME();
                //GETINVID();
                // GETADDEDINVID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETINVNAME();
                // GETRECORDS();
                //GETINVID();
                //GETADDEDINVID();
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
                DataSet ds = dal.GetSetINVDETAILS(Action: "GETINVAGAINSTSITE", INSTID: drpInstitute.SelectedValue, Project_ID: Drp_Project.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlINVNAME.DataSource = ds;
                    ddlINVNAME.DataTextField = "INVNAM";
                    ddlINVNAME.DataValueField = "INVCOD";
                    ddlINVNAME.DataBind();
                    ddlINVNAME.Items.Insert(0, new ListItem("--Select Investigator--", "0"));
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

        protected void bntSave_Click(object sender, EventArgs e)
        {
            dal = new DAL();
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "INSERTSITETOINVID",
                Project_ID: Drp_Project.SelectedValue,
                INSTID: drpInstitute.SelectedValue,
                INVCOD: ddlINVNAME.SelectedValue,
                INVID: txtInvId.Text,
                ADDRESS: txtAddress.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                STARTDATE: txtstartdate.Text,
                ENDDATE: txtenddate.Text,
                IPADDRESS: Comfun.GetIpAddress()
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
            dal = new DAL();
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "UPDATESITETOINVID",
                   INVID: txtInvId.Text,
                   ADDRESS: txtAddress.Text,
                   ENTEREDBY: Session["User_ID"].ToString(),
                   ID: Session["ASSIGNIDS"].ToString(),
                   STARTDATE: txtstartdate.Text,
                   ENDDATE: txtenddate.Text,
                   IPADDRESS: Comfun.GetIpAddress()
                   );
                ddlINVNAME.Visible = true;
                txtINVNAME.Visible = false;
                Drp_Project.Enabled = true;
                drpInstitute.Enabled = true;
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

        private void GETRECORDS()
        {
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "GETRECORDS",
                Project_ID: Drp_Project.SelectedValue,
                INSTID: drpInstitute.SelectedValue,
                INVCOD: ddlINVNAME.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdInvAdded.DataSource = ds;
                    grdInvAdded.DataBind();
                }
                else
                {
                    grdInvAdded.DataSource = null;
                    grdInvAdded.DataBind();
                }
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
                Session["ASSIGNIDS"] = ID;
                DataSet ds = dal.GetSetINVDETAILS(Action: "GETASSIGNINVDETAILSBYID", ID: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //fill_PROJECT();
                    Drp_Project.SelectedValue = ds.Tables[0].Rows[0]["Project_ID"].ToString();
                    Drp_Project.Enabled = false;
                    Drp_Project.CssClass = "form-control required  width250px";
                    //GETINSTITUTE();
                    drpInstitute.SelectedValue = ds.Tables[0].Rows[0]["INSTID"].ToString();
                    drpInstitute.Enabled = false;
                    drpInstitute.CssClass = "form-control required  width250px";
                    //GETINVNAME();
                    ddlINVNAME.Visible = false;
                    txtINVNAME.Text = ds.Tables[0].Rows[0]["INVNAM"].ToString();
                    txtINVNAME.ReadOnly = true;
                    txtINVNAME.Visible = true;
                    txtInvId.Text = ds.Tables[0].Rows[0]["INVID"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["INVPROJECTADDRESS"].ToString();
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
            DAL dal;
            dal = new DAL();

            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "DELETE_INV_ASSIGN", ID: ID, ENTEREDBY: Session["User_ID"].ToString(),
                         IPADDRESS: Comfun.GetIpAddress());
                GETRECORDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void CLEAR()
        {
            //Drp_Project.SelectedIndex = 0;
            drpInstitute.SelectedIndex = 0;
            ddlINVNAME.Items.Clear();
            txtInvId.Text = "";
            txtAddress.Text = "";
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


        protected void btnAssignINVExport_Click(object sender, EventArgs e)
        {
            try
            {
                Assign_INV_Master(header: "Assign Project Investigator Details", Action: "GET_AssignProJInv");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Assign_INV_Master(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
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