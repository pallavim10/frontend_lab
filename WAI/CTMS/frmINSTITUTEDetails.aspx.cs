using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace CTMS
{
    public partial class frmINSTITUTEDetails : System.Web.UI.Page
    {
        DAL dal;
        DAL constr = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_ID"] == null)
            {
                Response.Redirect("SessionExpired.aspx");
            }
            if (!this.IsPostBack)
            {
                // fill_drpdwn();
                fill_Country();
                GetRecords();
                btnUpdate.Visible = false;
                // GetRecords();
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

        public void GetRecords()
        {
            try
            {
                dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                ds = dal.GetSetINSTITUTEDETAILS(
                Action: "GET_INSTITUTE",
                CNTRYID: ddlCountry.SelectedItem.Value,
                ENTEREDBY: Session["User_ID"].ToString()
                );
                grdInstitute.Visible = true;
                grdInstitute.DataSource = ds.Tables[0];
                grdInstitute.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //private void addNewRecord(int rownum)
        //{
        //    try
        //    {
        //        CheckBox ChAction;
        //        ChAction = (CheckBox)InstituteMaster.Rows[rownum].FindControl("Chk_Sel_Fun");
        //        if (ChAction.Checked)
        //        {
        //            lblErrorMsg.Text = "";
        //            DAL dal;
        //            dal = new DAL();
        //            DataSet ds = dal.GetSetINSTITUTEDETAILS(
        //            Action: "INSERT_INST_DETAILS",
        //            Project_ID: Drp_Project.SelectedValue,
        //            INSTID: ((TextBox)InstituteMaster.Rows[rownum].FindControl("INSTID")).Text,
        //            ENTEREDBY: Session["User_ID"].ToString()
        //         );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        protected void bntSave_Click(object sender, EventArgs e)
        {
            string IPADDRESS = GetIpAddress();
            try
            {
                if (txtInstitute.Text != "")
                {
                    DAL dal;
                    dal = new DAL();
                    DataSet ds = dal.GetSetINSTITUTEDETAILS(
                    Action: "INSERT_INST_MASTER",
                    CNTRYID: ddlCountry.SelectedItem.Value,
                    State: ddlstate.SelectedValue,
                    INSTNAM: txtInstitute.Text,
                    AREA: txtAddress.Text,
                    CITY: ddlcity.Text,
                    ENTEREDBY: Session["User_ID"].ToString(),
                    Website: txtwebsites.Text,
                    IPADDRESS: IPADDRESS
                    );
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                }
                //int rownum;
                //for (rownum = 0; rownum < InstituteMaster.Rows.Count; rownum++)
                //{
                //    addNewRecord(rownum);
                //}

                GetRecords();
                Clear();

                //Response.Write("<script> alert('Record Updated successfully.');window.location='frmINSTITUTEDetails.aspx'; </script>");
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

        private void fill_INSTITUTE()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetINSTITUTEDETAILS(
                Action: "ALL_INSTITUTE",
                    // Project_Name: Drp_Project.SelectedItem.Text,
                ENTEREDBY: Session["User_ID"].ToString()
                );

                grdInstitute.DataSource = ds;
                grdInstitute.DataBind();
                grdInstitute.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //private void fill_drpdwn()
        //{
        //    try
        //    {
        //        DAL dal;
        //        dal = new DAL();
        //        DataSet ds = dal.GetSetPROJECTDETAILS(
        //        Action: "Get_Specific_Project",
        //        Project_ID: Convert.ToInt32(Session["PROJECTID"]),
        //        ENTEREDBY: Session["User_ID"].ToString()
        //        );
        //        lstProjects.DataSource = ds.Tables[0];
        //        lstProjects.DataValueField = "Project_ID";
        //        lstProjects.DataTextField = "PROJNAME";
        //        lstProjects.DataBind();
        //        lstProjects.Items.Insert(0, new ListItem("--Select Project--", "0"));
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

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

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                bntSave.Visible = true;
                btnUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Clear()
        {
            try
            {
                lstProjects.ClearSelection();
                ddlCountry.SelectedIndex = 0;
                ddlstate.Items.Clear();
                ddlcity.Items.Clear();
                txtInstitute.Text = "";
                txtAddress.Text = "";
                txtwebsites.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdInstitute_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string INSTID = Convert.ToString(e.CommandArgument);
                Session["INSTID"] = INSTID;
                if (e.CommandName == "EDITINST")
                {
                    EDITINST(INSTID);

                }
                else if (e.CommandName == "DELETEINST")
                {
                    DELETEINST(INSTID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void EDITINST(string INSTID)
        {
            try
            {
                DataSet ds = constr.GetSetINSTITUTEDETAILS(Action: "GET_INSTITUTEBYID", INSTID: INSTID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //fill_drpdwn();

                    ////lstProjects.ClearSelection();
                    //string PROJECTID = ds.Tables[0].Rows[0]["Project_ID"].ToString();
                    //if (PROJECTID != null)
                    //{
                    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //    {
                    //        if (PROJECTID != "0")
                    //        {
                    //            ListItem itm1 = lstProjects.Items.FindByValue(ds.Tables[0].Rows[i]["Project_ID"].ToString());
                    //            if (itm1 != null)
                    //                itm1.Selected = true;
                    //        }
                    //    }
                    //}

                    txtInstitute.Text = ds.Tables[0].Rows[0]["INSTNAM"].ToString();
                    fill_Country();
                    ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["CNTRYID"].ToString();
                    fill_State();
                    ddlstate.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
                    fill_City();
                    ddlcity.SelectedValue = ds.Tables[0].Rows[0]["CITY"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["AREA"].ToString();
                    txtwebsites.Text = ds.Tables[0].Rows[0]["WEBSITE"].ToString();
                    btnUpdate.Visible = true;
                    bntSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DELETEINST(string INSTID)
        {
            string IPADDRESS = GetIpAddress();
            try
            {

                DataSet ds = constr.GetSetINSTITUTEDETAILS(Action: "DELETEINST", INSTID: INSTID, ENTEREDBY: Session["User_ID"].ToString(), IPADDRESS: IPADDRESS);
                GetRecords();
                Clear();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstProjects_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    //    string PROJECTID = null;
                    //    foreach (ListItem item in lstProjects.Items)
                    //    {
                    //        if (item.Selected == true)
                    //        {
                    //            if (PROJECTID != null)
                    //            {
                    //                PROJECTID += "," + item.Value.ToString();
                    //            }
                    //            else
                    //            {
                    //                PROJECTID += item.Value.ToString();
                    //            }
                    //        }
                    //    }
                    string IPADDRESS = GetIpAddress();
                    if (txtInstitute.Text != "")
                    {
                        DAL dal;
                        dal = new DAL();
                        DataSet ds = dal.GetSetINSTITUTEDETAILS(
                        Action: "Update_INST_MASTER",
                        CNTRYID: ddlCountry.SelectedItem.Value,
                        State: ddlstate.SelectedValue,
                        INSTNAM: txtInstitute.Text,
                        AREA: txtAddress.Text,
                        CITY: ddlcity.Text,
                        ENTEREDBY: Session["User_ID"].ToString(),
                        INSTID: Session["INSTID"].ToString(),
                        Website: txtwebsites.Text,
                        IPADDRESS: IPADDRESS
                     );

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                    }

                    btnUpdate.Visible = false;
                    bntSave.Visible = true;
                    GetRecords();
                    Clear();

                    //Response.Write("<script> alert('Record Updated successfully.');window.location='frmINSTITUTEDetails.aspx'; </script>");
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdInstitute_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lbtndelete = (LinkButton)e.Row.FindControl("lbtndelete");
                    string COUNT = dr["count"].ToString();
                    if (COUNT != "0")
                    {
                        lbtndelete.Visible = false;
                    }
                    else
                    {
                        lbtndelete.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdInstitute_PreRender(object sender, EventArgs e)
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
        //lbSiteExport_Click

        protected void lbSiteExport_Click(object sender, EventArgs e)
        {
            try
            {
                SiteInv_Master(header: "Site Details", Action: "GET_SITE");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void SiteInv_Master(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
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