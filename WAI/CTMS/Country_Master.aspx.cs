using System;
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
    public partial class Country_Master : System.Web.UI.Page
    {
        CommonFunction.CommonFunction Comfun = new CommonFunction.CommonFunction();
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    fill_Country();
                    fill_State();
                    fill_City();
                    btnupdate.Visible = false;
                    btnupdateState.Visible = false;
                    btnupdateCity.Visible = false;
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
                grdCountry.DataSource = ds.Tables[0];
                grdCountry.DataBind();

                ddlcountry.DataSource = ds.Tables[0];
                ddlcountry.DataValueField = "CNTRYID";
                ddlcountry.DataTextField = "COUNTRY";
                ddlcountry.DataBind();
                ddlcountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
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
                CNTRYID: ddlcountry.SelectedValue
                );

                grdState.DataSource = ds.Tables[0];
                grdState.DataBind();

                ddlState.DataSource = ds.Tables[0];
                ddlState.DataValueField = "ID";
                ddlState.DataTextField = "StateName";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));
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
                STATEID: ddlState.SelectedValue
                );
                grdCity.DataSource = ds.Tables[0];
                grdCity.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnsubmitCountry_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "INSERT_COUNTRY",
                COUNTRYCOD: txtCountryCode.Text,
                COUNTRY: txtCountry.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Comfun. GetIpAddress()
                );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                fill_Country();
                txtCountry.Text = "";
                txtCountryCode.Text = "";                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "UPDATE_COUNTRY",
                CNTRYID: Session["CNTRYID"].ToString(),
                COUNTRYCOD: txtCountryCode.Text,
                COUNTRY: txtCountry.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                 IPADDRESS: Comfun.GetIpAddress()
                );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                fill_Country();
                txtCountry.Text = "";
                txtCountryCode.Text = "";

                btnsubmitCountry.Visible = true;
                btnupdate.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            txtCountry.Text = "";
            txtCountryCode.Text = "";
            btnsubmitCountry.Visible = true;
            btnupdate.Visible = false;
        }

        protected void btnsubState_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "INSERT_STATE",
                CNTRYID: ddlcountry.SelectedValue,
                STATENAME: txtState.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                ) ;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                fill_State();
                txtState.Text = "";
                ddlcountry.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateState_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "UPDATE_STATE",
                STATEID: Session["STATEID"].ToString(),
                CNTRYID: ddlcountry.SelectedValue,
                STATENAME: txtState.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                 IPADDRESS: Comfun.GetIpAddress()
                );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                fill_State();
                txtState.Text = "";
                ddlcountry.SelectedIndex = 0;
                btnsubState.Visible = true;
                btnupdate.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelState_Click(object sender, EventArgs e)
        {
            txtState.Text = "";
            ddlcountry.SelectedIndex = 0;
            btnsubState.Visible = true;
            btnupdateState.Visible = false;
        }

        protected void btnsubmitCity_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "INSERT_CITY",
                STATEID: ddlState.SelectedValue,
                CITYNAME: txtCity.Text,
                ENTEREDBY: Session["User_ID"].ToString()
                );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                fill_City();
                txtCity.Text = "";
                ddlState.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateCity_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "UPDATE_CITY",
                CITYID: Session["CITYID"].ToString(),
                STATEID: ddlState.SelectedValue,
                CITYNAME: txtCity.Text,
                ENTEREDBY: Session["User_ID"].ToString()
                );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                fill_City();
                txtCity.Text = "";
                ddlState.SelectedIndex = 0;
                btnsubmitCity.Visible = true;
                btnupdateCity.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelCity_Click(object sender, EventArgs e)
        {
            txtCity.Text = "";
            ddlState.SelectedIndex = 0;
            btnsubmitCity.Visible = true;
            btnupdateCity.Visible = false;
        }

        protected void grdCountry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                Session["CNTRYID"] = ID;

                if (e.CommandName == "EditCOUNTRY")
                {
                    EDITCOUNTRY();
                }
                else if (e.CommandName == "DeleteCOUNTRY")
                {
                    DELETECOUNTRY();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void EDITCOUNTRY()
        {
            try
            {
                DataSet ds = dal.GetSetCOUNTRYDETAILS(Action: "GET_COUNTRY_BYID", CNTRYID: Session["CNTRYID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCountry.Text = ds.Tables[0].Rows[0]["COUNTRY"].ToString();
                    txtCountryCode.Text = ds.Tables[0].Rows[0]["COUNTRYCOD"].ToString();
                    btnsubmitCountry.Visible = false;
                    btnupdate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETECOUNTRY()
        {
            try
            {
                DataSet ds = dal.GetSetCOUNTRYDETAILS(Action: "DELETE_COUNTRY", CNTRYID: Session["CNTRYID"].ToString(), 
                    ENTEREDBY: Session["User_ID"].ToString(),
                    IPADDRESS: Comfun.GetIpAddress()

                    );

                fill_Country();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdState_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                Session["STATEID"] = ID;

                if (e.CommandName == "EditSTATE")
                {
                    EDITSTATE();
                }
                else if (e.CommandName == "DeleteSTATE")
                {
                    DELETESTATE();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void EDITSTATE()
        {
            try
            {
                DataSet ds = dal.GetSetCOUNTRYDETAILS(Action: "GET_STATE_BYID", STATEID: Session["STATEID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcountry.SelectedValue = ds.Tables[0].Rows[0]["CountryID"].ToString();
                    txtState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
                    btnsubState.Visible = false;
                    btnupdateState.Visible = true;
                
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETESTATE()
        {
            try
            {
                DataSet ds = dal.GetSetCOUNTRYDETAILS(Action: "DELETE_STATE", STATEID: Session["STATEID"].ToString(), 
                    ENTEREDBY: Session["User_ID"].ToString(),
                    IPADDRESS: Comfun.GetIpAddress()
                    );

                fill_State();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdCity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                Session["CITYID"] = ID;

                if (e.CommandName == "EDITCITY")
                {
                    EDITCITY();
                }
                else if (e.CommandName == "DeleteCITY")
                {
                    DELETECITY();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void EDITCITY()
        {
            try
            {
                DataSet ds = dal.GetSetCOUNTRYDETAILS(Action: "GET_CITY_BYID", CITYID: Session["CITYID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlState.SelectedValue = ds.Tables[0].Rows[0]["state_id"].ToString();
                    txtCity.Text = ds.Tables[0].Rows[0]["city_name"].ToString();
                    btnsubmitCity.Visible = false;
                    btnupdateCity.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETECITY()
        {
            try
            {
                DataSet ds = dal.GetSetCOUNTRYDETAILS(Action: "DELETE_CITY", CITYID: Session["CITYID"].ToString(), ENTEREDBY: Session["User_ID"].ToString());

                fill_City();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void grdCountry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string STATE_COUNTS = drv["STATE_COUNTS"].ToString();

                    LinkButton lbtndelete = (e.Row.FindControl("lbtndelete") as LinkButton);

                    if (STATE_COUNTS == "0")
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

        protected void grdState_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string CITY_COUNTS = drv["CITY_COUNTS"].ToString();

                    LinkButton lbtndeleteState = (e.Row.FindControl("lbtndeleteState") as LinkButton);

                    if (CITY_COUNTS == "0")
                    {
                        lbtndeleteState.Visible = true;
                    }

                    else
                    {
                        lbtndeleteState.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_State();
                fill_City();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
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

        
        protected void btnMangConExport_Click(object sender, EventArgs e)
        {
            try
            {
                Master_Country(header: "County_Master", Action: "GET_COUNTY_MASTER");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCountyExport_Click(object sender, EventArgs e)
        {
            try
            {
                Master_Country(header: "Country", Action: "GET_County");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnstateExport_Click(object sender, EventArgs e)
        {
            try
            {
                Master_Country(header: "State", Action: "GET_STATE", id: ddlcountry.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCityExport_Click(object sender, EventArgs e)
        {
            try
            {
                Master_Country(header:"City",Action: "GET_CITY",id: ddlState.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Master_Country(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null,string id=null)
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.Export_Data_SP(Action: Action, PRODUCTID: PRODUCT_ID, SUBCLASSID: SUBCLASS_ID,ID:id);

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