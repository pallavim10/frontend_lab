using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;

namespace CTMS
{
    public partial class Sponsor_Add : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction Comfun = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lbtnUpdate.Visible = false;
                    bind_Sposnor();
                    fill_Country();
                }
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
        private void bind_Sposnor()
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(Action: "GET_Sponsors");
                gvSponsor.DataSource = ds.Tables[0];
                gvSponsor.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void insert()
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(
                Action: "INSERT_Sposnor",
                Company: txtCompanyName.Text,
                ContactNo: txtContactNo.Text,
                Website: txtWebsite.Text,
                Country: ddlCountry.SelectedValue,
                State: ddlstate.SelectedValue,
                City: ddlcity.SelectedValue,
                Address: txtAddress.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                txtCompanyName.Text = "";
                txtContactNo.Text = "";
                txtWebsite.Text = "";
                ddlCountry.SelectedIndex = 0;
                ddlstate.Items.Clear();
                ddlcity.Items.Clear();
                txtAddress.Text = "";

                bind_Sposnor();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void update()
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(
                Action: "UPDATE_Sponsor",
                Company: txtCompanyName.Text,
                ContactNo: txtContactNo.Text,
                Website: txtWebsite.Text,
                Country: ddlCountry.SelectedValue,
                State: ddlstate.SelectedValue,
                City: ddlcity.SelectedValue,
                Address: txtAddress.Text,
                ID: Session["SPONSORID"].ToString(),
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                txtCompanyName.Text = "";
                txtContactNo.Text = "";
                txtWebsite.Text = "";
                ddlCountry.SelectedIndex = 0;
                ddlstate.Items.Clear();
                ddlcity.Items.Clear();
                txtAddress.Text = "";

                lbtnSubmit.Visible = true;
                lbtnUpdate.Visible = false;

                bind_Sposnor();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void delete(string ID)
        {
            try
            {
                dal.Sponsor_SP(Action: "DELETE_Sponsor", ID: ID, ENTEREDBY: Session["User_ID"].ToString(),
                   IPADDRESS: Comfun.GetIpAddress());

                bind_Sposnor();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void edit(string ID)
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(Action: "SELECT_Sponsor", ID: ID);

                txtCompanyName.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                txtWebsite.Text = ds.Tables[0].Rows[0]["Website"].ToString();
                fill_Country();
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["Country"].ToString();
                fill_State();
                ddlstate.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
                fill_City();
                ddlcity.SelectedValue = ds.Tables[0].Rows[0]["City"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();

                lbtnUpdate.Visible = true;
                lbtnSubmit.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                insert();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                update();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtCompanyName.Text = "";
                txtContactNo.Text = "";
                txtWebsite.Text = "";
                ddlCountry.SelectedIndex = 0;
                ddlstate.Items.Clear();
                ddlcity.Items.Clear();
                txtAddress.Text = "";
                lbtnSubmit.Visible = true;
                lbtnUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void gvSponsor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["SPONSORID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete(id);
                    bind_Sposnor();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void gvSponsor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteSposnor = (e.Row.FindControl("lbtndeleteSposnor") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        //lbtndeleteSposnor.Visible = false;
                        lbtndeleteSposnor.Visible = true;

                    }
                    else
                    {
                        lbtndeleteSposnor.Visible = true;
                    }
                }
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
                fill_City();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_State();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void gvSponsor_PreRender(object sender, EventArgs e)
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
        protected void btnAddSponsorExport_Click(object sender, EventArgs e)
        {
            try
            {
                Sponsor_Master(header: "Sponsor Details", Action: "GET_Sponsor");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Sponsor_Master(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
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