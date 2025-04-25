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
    public partial class Emp_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                FileUpload1.Attributes["multiple"] = "multiple";

                if (!IsPostBack)
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        view_EmpDetails(Request.QueryString["ID"].ToString());
                    }
                    else
                    {
                        int EmpCode = GetEmplCode();
                        txtEmpCode.Text = Convert.ToString(EmpCode);
                    }
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

        private void view_EmpDetails(string ID)
        {
            try
            {
                DataSet ds = dal.EmpMaster_SP(Action: "SELECT", ID: ID);
                txtEmpCode.Text = ds.Tables[0].Rows[0]["EmpCode"].ToString();
                txtFirstName.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                txtLastName.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
                txtEmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                txtJobTitle.Text = ds.Tables[0].Rows[0]["JobTitle"].ToString();
                txtBusPhone.Text = ds.Tables[0].Rows[0]["BusinessPhone"].ToString();
                txtHomePhone.Text = ds.Tables[0].Rows[0]["HomePhone"].ToString();
                txtMobilePhone.Text = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                txtFax.Text = ds.Tables[0].Rows[0]["FaxNumber"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                fill_Country();
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["Country"].ToString();
                fill_State();
                ddlstate.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
                fill_City();
                ddlcity.SelectedValue = ds.Tables[0].Rows[0]["City"].ToString();
                txtPostal.Text = ds.Tables[0].Rows[0]["Postal"].ToString();
                txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                txtCompany.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                txtpersonalEmailid.Text = ds.Tables[0].Rows[0]["PersonalEmailId"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_EmpDetails()
        {
            DataSet dsemp = new DataSet();
                
            try
            {
                DataSet ds = new DataSet();

                if (Request.QueryString["ID"] != null)
                {
                    dsemp = GetEmplCode_byId(id:Convert.ToString(txtEmpCode.Text));
                    if (dsemp.Tables[0].Rows.Count > 0)
                    {
                        if (dsemp.Tables[0].Rows[0]["EmpCode"].ToString() == txtEmpCode.Text && dsemp.Tables[0].Rows[0]["EmpCode"].ToString()== Request.QueryString["ID"].ToString())
                        {
                            ds = dal.EmpMaster_SP(Action: "INSERT",
                        ID: Request.QueryString["ID"].ToString(),
                        EmpCode: txtEmpCode.Text,
                        FirstName: txtFirstName.Text,
                        LastName: txtLastName.Text,
                        EmailID: txtEmailID.Text,
                        JobTitle: txtJobTitle.Text,
                        BusinessPhone: txtBusPhone.Text,
                        HomePhone: txtHomePhone.Text,
                        MobilePhone: txtMobilePhone.Text,
                        FaxNumber: txtFax.Text,
                        Address: txtAddress.Text,
                        City: ddlcity.SelectedValue,
                        State: ddlstate.SelectedValue,
                        Postal: txtPostal.Text,
                        Country: ddlCountry.SelectedValue,
                        Notes: txtNotes.Text,
                        Company: txtCompany.Text,
                        PersonalEmailId: txtpersonalEmailid.Text,
                        ENTEREDBY: Session["User_ID"].ToString(),
                        IPADDRESS: GetIpAddress()
                        );
                            string ID = ds.Tables[0].Rows[0]["ID"].ToString();
                            upload_Attachments(ID);
                            Response.Redirect("View_EmpDetails.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Employee Code Exist')", true);
                        }
                    }
                    else
                    {
                        ds = dal.EmpMaster_SP(Action: "INSERT",
                        ID: Request.QueryString["ID"].ToString(),
                        EmpCode: txtEmpCode.Text,
                        FirstName: txtFirstName.Text,
                        LastName: txtLastName.Text,
                        EmailID: txtEmailID.Text,
                        JobTitle: txtJobTitle.Text,
                        BusinessPhone: txtBusPhone.Text,
                        HomePhone: txtHomePhone.Text,
                        MobilePhone: txtMobilePhone.Text,
                        FaxNumber: txtFax.Text,
                        Address: txtAddress.Text,
                        City: ddlcity.SelectedValue,
                        State: ddlstate.SelectedValue,
                        Postal: txtPostal.Text,
                        Country: ddlCountry.SelectedValue,
                        Notes: txtNotes.Text,
                        Company: txtCompany.Text,
                        PersonalEmailId: txtpersonalEmailid.Text,
                        ENTEREDBY: Session["User_ID"].ToString(),
                        IPADDRESS: GetIpAddress()
                        );
                        string ID = ds.Tables[0].Rows[0]["ID"].ToString();
                        upload_Attachments(ID);
                        Response.Redirect("View_EmpDetails.aspx");

                    }
                }
                else
                {
                    dsemp = GetEmplCode_byId(Convert.ToString(txtEmpCode.Text));
                    if(dsemp.Tables[0].Rows.Count<1)
                    {
                        ds = dal.EmpMaster_SP(Action: "INSERT",
                        EmpCode: txtEmpCode.Text,
                        FirstName: txtFirstName.Text,
                        LastName: txtLastName.Text,
                        EmailID: txtEmailID.Text,
                        JobTitle: txtJobTitle.Text,
                        BusinessPhone: txtBusPhone.Text,
                        HomePhone: txtHomePhone.Text,
                        MobilePhone: txtMobilePhone.Text,
                        FaxNumber: txtFax.Text,
                        Address: txtAddress.Text,
                        City: ddlcity.SelectedValue,
                        State: ddlstate.SelectedValue,
                        Postal: txtPostal.Text,
                        Country: ddlCountry.SelectedValue,
                        Notes: txtNotes.Text,
                        Company: txtCompany.Text,
                        PersonalEmailId: txtpersonalEmailid.Text,
                        ENTEREDBY: Session["User_ID"].ToString()
                        );
                        string ID = ds.Tables[0].Rows[0]["ID"].ToString();
                        upload_Attachments(ID);

                        Response.Redirect("View_EmpDetails.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Employee Code Exist')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Clear()
        {
            try
            {
                txtEmpCode.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmailID.Text = "";
                txtJobTitle.Text = "";
                txtBusPhone.Text = "";
                txtHomePhone.Text = "";
                txtMobilePhone.Text = "";
                txtFax.Text = "";
                txtAddress.Text = "";
                ddlCountry.SelectedIndex = 0;
                ddlstate.Items.Clear();
                ddlcity.Items.Clear();
                txtPostal.Text = "";
                txtNotes.Text = "";
                txtCompany.Text = "";
                txtpersonalEmailid.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void upload_Attachments(string ID)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < Request.Files.Count; i++)

                {
                    HttpPostedFile uploadedFile = Request.Files[i];

                    string filename = Path.GetFileName(uploadedFile.FileName);

                    if (filename != "")
                    {
                        string contentType = uploadedFile.ContentType;

                        using (Stream fs = uploadedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                dal.EmpMaster_SP(Action: "Upload", EmpCode: ID, FileName: filename, ContentType: contentType, Data: bytes, ENTEREDBY: Session["User_ID"].ToString());
                            }
                        }
                    }
                }
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
                insert_EmpDetails();
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
                Clear();
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

        private int GetEmplCode()
        {
            int count = 0;
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = dal.EmpMaster_SP(Action: "Get_Emp_Code");
                dt = ds.Tables[0];
                count = Convert.ToInt32(dt.Rows[0]["EmpCode"]);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return count;
        }
        private DataSet GetEmplCode_byId(string id=null,string EmpID=null)
        {
            DataSet ds = new DataSet();
             try
            {
                DataTable dt = new DataTable();
                ds = dal.EmpMaster_SP(Action: "Get_Emp_Code_ByID", EmpCode: id);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return ds;
        }

    }
}