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
    public partial class frmEthicsCommity : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx");
                }
                if (!this.IsPostBack)
                {
                    fill_Country();
                    GETINSTITUTE();
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
                ds = dal.GetSetEthicsCommityDETAILS(
                Action: "GETETHICSDATA",
                ENTEREDBY: Session["User_ID"].ToString(),
                INSTID: drpInstitute.SelectedValue
                );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdEthicsComm.DataSource = ds.Tables[0];
                    grdEthicsComm.DataBind();
                }
                else
                {
                    grdEthicsComm.DataSource = null;
                    grdEthicsComm.DataBind();
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
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetEthicsCommityDETAILS(
                Action: "INSERT_ETHICS_MASTER",
                INSTID: drpInstitute.SelectedValue,
                ETHICSNAME: txtEthicName.Text,
                ETHICSQUAL: txtEthicQual.Text,
                ETHICSSPEC: txtEthicSpec.Text, MOBNO: txtMobileNo.Text, ADDRESS: txtAddress.Text,
                TELNO: txtTelNo.Text, FAXNO: txtFaxNo.Text, EMAILID: txtEmailId.Text, STATUS: "ACTIVE",
                ENTEREDBY: Session["User_ID"].ToString(),
                State: ddlstate.SelectedValue,
                City: ddlcity.SelectedValue,
                CNTRYID: ddlCountry.SelectedValue,
                STARTDATE: txtstartdate.Text,
                ENDDATE: txtenddate.Text
             );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                GetRecords();
                CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            CLEAR();
            bntSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void grdEthicsComm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = Convert.ToString(e.CommandArgument);
                if (e.CommandName == "EDITETHICS")
                {
                    EDITETHICSCOMMITY(ID);
                }
                else if (e.CommandName == "Delete1")
                {
                    DELETEETHICSCOMMITY(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDITETHICSCOMMITY(string ID)
        {
            try
            {
                Session["ETHICID"] = ID;
                DataSet ds = dal.GetSetEthicsCommityDETAILS(Action: "GETETHICSBYID", ID: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpInstitute.SelectedValue = ds.Tables[0].Rows[0]["EthicsInstitute"].ToString();
                    txtEthicName.Text = ds.Tables[0].Rows[0]["EthicsNAM"].ToString();
                    txtEthicQual.Text = ds.Tables[0].Rows[0]["EthicsQUAL"].ToString();
                    txtEthicSpec.Text = ds.Tables[0].Rows[0]["EthicsSPEC"].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0]["MOBNO"].ToString();
                    txtTelNo.Text = ds.Tables[0].Rows[0]["TELNO"].ToString();
                    txtFaxNo.Text = ds.Tables[0].Rows[0]["FAXNO"].ToString();
                    txtEmailId.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    fill_Country();
                    ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["CNTRYID"].ToString();
                    fill_State();
                    ddlstate.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
                    fill_City();
                    ddlcity.SelectedValue = ds.Tables[0].Rows[0]["City"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["address"].ToString();
                    txtstartdate.Text = ds.Tables[0].Rows[0]["STARTDAT"].ToString();
                    txtenddate.Text = ds.Tables[0].Rows[0]["ENDDATE"].ToString();

                    bntSave.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETEETHICSCOMMITY(string ID)
        {
            try
            {
                DataSet ds = dal.GetSetEthicsCommityDETAILS(Action: "DELETE_ETHICS_MASTER", ID: ID);

                GetRecords();
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
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetEthicsCommityDETAILS(
                Action: "Update_Ethic_MASTER",
                ID: Session["ETHICID"].ToString(),
                INSTID: drpInstitute.SelectedValue,
                ETHICSNAME: txtEthicName.Text,
                ETHICSQUAL: txtEthicQual.Text,
                ETHICSSPEC: txtEthicSpec.Text, MOBNO: txtMobileNo.Text, ADDRESS: txtAddress.Text,
                TELNO: txtTelNo.Text, FAXNO: txtFaxNo.Text, EMAILID: txtEmailId.Text, STATUS: "ACTIVE",
                ENTEREDBY: Session["User_ID"].ToString(),
                State: ddlstate.SelectedValue,
                City: ddlcity.SelectedValue,
                CNTRYID: ddlCountry.SelectedValue,
                STARTDATE: txtstartdate.Text,
                ENDDATE: txtenddate.Text
             );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                bntSave.Visible = true;
                btnUpdate.Visible = false;
                GetRecords();
                CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR()
        {
            drpInstitute.SelectedIndex = 0;
            txtEthicName.Text = "";
            txtEthicQual.Text = "";
            txtEthicSpec.Text = "";
            txtMobileNo.Text = "";
            txtTelNo.Text = "";
            txtFaxNo.Text = "";
            txtEmailId.Text = "";
            txtAddress.Text = "";
            txtstartdate.Text = "";
            txtenddate.Text = "";
            ddlCountry.SelectedIndex = 0;
            ddlstate.Items.Clear();
            ddlcity.Items.Clear();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRecords();
                fill_State();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRecords();
                fill_City();
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
                Action: "GET_COUNTRY"
                );
                ddlCountry.DataSource = ds.Tables[0];
                ddlCountry.DataValueField = "CNTRYID";
                ddlCountry.DataTextField = "COUNTRY";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void fill_State()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "GET_STATE",
                CNTRYID: ddlCountry.SelectedValue
                );
                ddlstate.DataSource = ds.Tables[0];
                ddlstate.DataValueField = "ID";
                ddlstate.DataTextField = "StateName";
                ddlstate.DataBind();
                ddlstate.Items.Insert(0, new ListItem("--Select State--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void fill_City()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "GET_CITY",
                STATEID: ddlstate.SelectedValue
                );
                ddlcity.DataSource = ds.Tables[0];
                ddlcity.DataValueField = "city_id";
                ddlcity.DataTextField = "city_name";
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, new ListItem("--Select City--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void GETINSTITUTE()
        {
            try
            {
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

        protected void drpInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRecords();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdEthicsComm_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdEthicsComm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lbtndelete = (LinkButton)e.Row.FindControl("lbtndeleteEmp");
                    string COUNT = dr["count"].ToString();
                    if (COUNT == "0")
                    {
                        lbtndelete.Visible = true;
                    }
                    else
                    {
                        lbtndelete.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbECdetailExport_Click(object sender, EventArgs e)
        {
            try
            {
                Ethics_Committee_Details(header: "Ethics Committee Details", Action: "Get_Ethics_Committee_Details");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Ethics_Committee_Details(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
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