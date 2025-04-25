using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CTMS;
using System.Net;
using System.Net.Sockets;
using CTMS.CommonFunction;

namespace PPT
{
    public partial class frmINVDETAILS : System.Web.UI.Page
    {
        DAL dal;
        DAL constr = new DAL();
        CommonFunction Comfun = new CommonFunction();
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
                    //fill_Project();
                    fill_Country();
                    GETINSTITUTE();
                    GetRecords();
                }
                ViewState["User_ID"] = Session["User_ID"].ToString();
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
                ds = dal.GetSetINVDETAILS(
                Action: "GETINVID",
                ENTEREDBY: Session["User_ID"].ToString()
                );
                grdInvAdded.DataSource = ds.Tables[0];
                grdInvAdded.DataBind();
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
                lstINST.DataSource = ds.Tables[0];
                lstINST.DataValueField = "INSTID";
                lstINST.DataTextField = "INSTNAM";
                lstINST.DataBind();
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
                string INSTID = null;

                foreach (ListItem item in lstINST.Items)
                {
                    if (item.Selected == true)
                    {
                        if (INSTID != null)
                        {
                            INSTID += "," + item.Value.ToString();
                        }
                        else
                        {
                            INSTID += item.Value.ToString();
                        }
                    }
                }

                if (txtInvName.Text != "")
                {
                    DAL dal;
                    dal = new DAL();
                    DataSet ds = dal.GetSetINVDETAILS(
                    Action: "INSERT_INV_MASTER",
                    INVNAME: txtInvName.Text,
                    INVQUAL: txtInvQual.Text,
                    INSTID: INSTID,
                    INVSPEC: txtInvSpec.Text, MOBNO: txtMobileNo.Text, ADDRESS: txtAddress.Text,
                    TELNO: txtTelNo.Text, FAXNO: txtFaxNo.Text, EMAILID: txtEmailId.Text, STATUS: "ACTIVE",
                    ENTEREDBY: Session["User_ID"].ToString(),
                    State: ddlstate.SelectedValue,
                    City: ddlcity.SelectedValue,
                    CNTRYID: ddlCountry.SelectedValue,
                    CONTTM: txtCont.Text,
                    STARTDATE: txtstartdate.Text,
                    ENDDATE: txtenddate.Text,
                    IPADDRESS: Comfun.GetIpAddress()
                 );

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                }

                GetRecords();
                fill_Country();
                CLEAR();
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
                Session["INVCOD"] = ID;
                if (e.CommandName == "EDITINV")
                {
                    EDITINVDETAILS(ID);
                }
                else if (e.CommandName == "Delete1")
                {
                    DELETEINVDETAILS(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDITINVDETAILS(string ID)
        {
            try
            {
                DataSet ds = constr.GetSetINVDETAILS(Action: "GETINVIDBYID", INVCOD: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GETINSTITUTE();

                    string[] INSTID = ds.Tables[0].Rows[0]["INSTID"].ToString().Split(',').ToArray();
                    lstINST.ClearSelection();
                    if (INSTID != null && INSTID.Length > 0)
                    {
                        for (int i = 0; i < INSTID.Length; i++)
                        {
                            if (INSTID[i] != "")
                            {
                                ListItem itm2 = lstINST.Items.FindByValue(INSTID[i]);
                                if (itm2 != null)
                                    itm2.Selected = true;
                            }
                        }
                    }

                    txtInvName.Text = ds.Tables[0].Rows[0]["INVNAM"].ToString();
                    txtInvQual.Text = ds.Tables[0].Rows[0]["INVQUAL"].ToString();
                    txtInvSpec.Text = ds.Tables[0].Rows[0]["INVSPEC"].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0]["MOBNO"].ToString();
                    txtTelNo.Text = ds.Tables[0].Rows[0]["TELNO"].ToString();
                    txtFaxNo.Text = ds.Tables[0].Rows[0]["FAXNO"].ToString();
                    txtEmailId.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    txtCont.Text = ds.Tables[0].Rows[0]["CONTTM"].ToString();
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

        private void DELETEINVDETAILS(string ID)
        {
            DAL dal;
            dal = new DAL();
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "DELETE_INV_MASTER", ID: ID, ENTEREDBY: Session["User_ID"].ToString(),
                    IPADDRESS: Comfun.GetIpAddress());

                fill_Country();
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
                string INSTID = null;

                foreach (ListItem item in lstINST.Items)
                {
                    if (item.Selected == true)
                    {
                        if (INSTID != null)
                        {
                            INSTID += "," + item.Value.ToString();
                        }
                        else
                        {
                            INSTID += item.Value.ToString();
                        }
                    }
                }
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetINVDETAILS(
                Action: "Update_INV_MASTER",
                INVNAME: txtInvName.Text,
                INVQUAL: txtInvQual.Text,
                INSTID: INSTID,
                INVSPEC: txtInvSpec.Text, MOBNO: txtMobileNo.Text, ADDRESS: txtAddress.Text,
                TELNO: txtTelNo.Text, FAXNO: txtFaxNo.Text, EMAILID: txtEmailId.Text, STATUS: "ACTIVE",
                ENTEREDBY: Session["User_ID"].ToString(),
                State: ddlstate.SelectedValue,
                City: ddlcity.SelectedValue,
                CNTRYID: ddlCountry.SelectedValue,
                CONTTM: txtCont.Text,
                INVCOD: Session["INVCOD"].ToString(),
                STARTDATE: txtstartdate.Text,
                ENDDATE: txtenddate.Text,
                 IPADDRESS: Comfun.GetIpAddress()

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
            txtInvName.Text = "";
            txtInvQual.Text = "";
            txtInvSpec.Text = "";
            txtMobileNo.Text = "";
            txtTelNo.Text = "";
            txtFaxNo.Text = "";
            txtEmailId.Text = "";
            txtCont.Text = "";
            txtAddress.Text = "";
            txtstartdate.Text = "";
            txtenddate.Text = "";
            ddlCountry.Items.Clear();
            ddlstate.Items.Clear();
            ddlcity.Items.Clear();
            lstINST.ClearSelection();
            fill_Country();
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            CLEAR();
            bntSave.Visible = true;
            btnUpdate.Visible = false;
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

                // fill_Institute();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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

        protected void grdInvAdded_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lbtndelete = (LinkButton)e.Row.FindControl("lbtndelete");
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

        protected void btnINVDetailsExport_Click(object sender, EventArgs e)
        {
            try
            {
                INV_Master(header: "Investigator Details", Action: "GET_InvDetails");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void INV_Master(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
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