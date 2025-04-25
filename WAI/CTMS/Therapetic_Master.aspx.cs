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
    public partial class Therapetic_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction Comfun = new CommonFunction.CommonFunction();   
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Class();
                    btnUpdateClass.Visible = false;
                    btnUpdateSubClass.Visible = false;
                    btnUpdateIndic.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Therapectic Class

        private void insert_Class()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "INSERT",
                PRODUCTNAM: txtClass.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                txtClass.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Class(string ID)
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_SINGLE",
                PRODUCTID: ID
                    );
                txtClass.Text = ds.Tables[0].Rows[0]["PRODUCTNAM"].ToString();
                Session["CLASSID"] = ID;
                btnSubmitClass.Visible = false;
                btnUpdateClass.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Class()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "UPDATE",
                PRODUCTNAM: txtClass.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                PRODUCTID: Session["CLASSID"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                    ) ;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                txtClass.Text = "";
                btnSubmitClass.Visible = true;
                btnUpdateClass.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Class(string ID)
        {
            try
            {
                dal.GetSetPRODUCTDETAILS(
                Action: "DELETE",
                PRODUCTID: ID, ENTEREDBY: Session["User_ID"].ToString(),
                 IPADDRESS: Comfun.GetIpAddress()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Class()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_DATA"
                    );
                gvClass.DataSource = ds.Tables[0];
                gvClass.DataBind();

                ddlClass.DataSource = ds.Tables[0];
                ddlClass.DataValueField = "PRODUCTID";
                ddlClass.DataTextField = "PRODUCTNAM";
                ddlClass.DataBind();
                ddlClass.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Class_GV()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_DATA"
                    );
                gvClass.DataSource = ds.Tables[0];
                gvClass.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Therapectic Sub-Class

        private void insert_SubClass()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "INSERT_Sub",
                PRODUCTID: ddlClass.SelectedValue,
                PRODUCTNAM: txtSubClass.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                    );
                txtSubClass.Text = "";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_SubClass(string ID)
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_SINGLE_Sub",
                ID: ID
                    );
                txtSubClass.Text = ds.Tables[0].Rows[0]["Therapetic_SubClass"].ToString();
                ddlClass.SelectedValue = ds.Tables[0].Rows[0]["PRODUCTID"].ToString(); ;
                Session["SUBCLASSID"] = ID;
                btnSubmitSubClass.Visible = false;
                btnUpdateSubClass.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_SubClass()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "UPDATE_Sub",
                PRODUCTNAM: txtSubClass.Text,
                ID: Session["SUBCLASSID"].ToString(),
                ENTEREDBY: Session["User_ID"].ToString(),
                 IPADDRESS: Comfun.GetIpAddress()
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);

                txtSubClass.Text = "";
                btnSubmitSubClass.Visible = true;
                btnUpdateSubClass.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_SubClass(string ID)
        {
            try
            {
                dal.GetSetPRODUCTDETAILS(
                Action: "DELETE_Sub",
                ID: ID, ENTEREDBY: Session["User_ID"].ToString(),
                 IPADDRESS: Comfun.GetIpAddress()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_SubClass()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_Sub",
                PRODUCTID: ddlClass.SelectedValue
                    );
                gvSubClass.DataSource = ds.Tables[0];
                gvSubClass.DataBind();

                ddlSubClass.DataSource = ds.Tables[0];
                ddlSubClass.DataValueField = "ID";
                ddlSubClass.DataTextField = "Therapetic_SubClass";
                ddlSubClass.DataBind();
                ddlSubClass.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_SubClass_GV()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_Sub",
                PRODUCTID: ddlClass.SelectedValue
                    );
                gvSubClass.DataSource = ds.Tables[0];
                gvSubClass.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Therapectic Sub-Class

        private void insert_Indic()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "INSERT_Indic",
                PRODUCTID: ddlClass.SelectedValue,
                ID: ddlSubClass.SelectedValue,
                PRODUCTNAM: txtIndic.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS : Comfun.GetIpAddress()
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                txtIndic.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Indic(string ID)
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_SINGLE_Indic",
                ID: ID
                    );
                txtIndic.Text = ds.Tables[0].Rows[0]["INDICATION"].ToString();
                ddlSubClass.SelectedValue = ds.Tables[0].Rows[0]["SUBCLASSID"].ToString(); ;
                Session["INDICID"] = ID;
                btnSubmitIndic.Visible = false;
                btnUpdateIndic.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Indic()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "UPDATE_Indic",
                PRODUCTNAM: txtIndic.Text,
                ID: Session["INDICID"].ToString(),
                ENTEREDBY: Session["User_ID"].ToString(),
                 IPADDRESS: Comfun.GetIpAddress()
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);

                txtIndic.Text = "";
                btnSubmitIndic.Visible = true;
                btnUpdateIndic.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Indic(string ID)
        {
            try
            {
                dal.GetSetPRODUCTDETAILS(
                Action: "DELETE_Indic",
                ID: ID, ENTEREDBY: Session["User_ID"].ToString(),
                 IPADDRESS: Comfun.GetIpAddress()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Indic()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_Indic",
                ID: ddlSubClass.SelectedValue
                    );
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitClass_Click(object sender, EventArgs e)
        {
            insert_Class();
            bind_Class();
        }

        protected void btnUpdateClass_Click(object sender, EventArgs e)
        {
            update_Class();
            bind_Class();
        }

        protected void btnCancelClass_Click(object sender, EventArgs e)
        {
            txtClass.Text = "";
            btnSubmitClass.Visible = true;
            btnUpdateClass.Visible = false;
        }

        protected void gvClass_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                if (e.CommandName == "Edit1")
                {
                    edit_Class(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Class(id);
                    bind_Class();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvClass_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteClass = (e.Row.FindControl("lbtndeleteClass") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteClass.Visible = false;
                    }
                    else
                    {
                        lbtndeleteClass.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_SubClass();
        }

        protected void btnSubmitSubClass_Click(object sender, EventArgs e)
        {
            insert_SubClass();
            bind_SubClass();
            bind_Class_GV();
        }

        protected void btnUpdateSubClass_Click(object sender, EventArgs e)
        {
            update_SubClass();
            bind_SubClass();
        }

        protected void btnCancelSubClass_Click(object sender, EventArgs e)
        {
            txtSubClass.Text = "";
            ddlSubClass.SelectedIndex = 0;
            btnSubmitSubClass.Visible = true;
            btnUpdateSubClass.Visible = false;
        }

        protected void gvSubClass_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                if (e.CommandName == "Edit1")
                {
                    edit_SubClass(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_SubClass(id);
                    bind_SubClass();
                    bind_Class_GV();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSubClass_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteSubClass = (e.Row.FindControl("lbtndeleteSubClass") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteSubClass.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSubClass.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSubClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_Indic();
        }

        protected void btnSubmitIndic_Click(object sender, EventArgs e)
        {
            insert_Indic();
            bind_SubClass_GV();
            bind_Indic();
        }

        protected void btnUpdateIndic_Click(object sender, EventArgs e)
        {
            update_Indic();
            bind_Indic();
        }

        protected void btnCancelIndic_Click(object sender, EventArgs e)
        {

            txtIndic.Text = "";
            ddlSubClass.SelectedIndex = 0;
            btnSubmitIndic.Visible = true;
            btnUpdateIndic.Visible = false;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                if (e.CommandName == "Edit1")
                {
                    edit_Indic(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Indic(id);
                    bind_Indic();
                    bind_SubClass_GV();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnTharaMasterExport_Click(object sender, EventArgs e)
        {
            try
            {
                Therapeutic_Masters(header: "Therapeutic Masters");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnTharaExport_Click(object sender, EventArgs e)
        {
            try
            {
                Therapeutic_class(header: "Therapeutic");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnTharaSubExport_Click(object sender, EventArgs e)
        {
            try
            {
                Therapeutic_subClass(header: "Therapeutic Sub Class", Action: "GET_Sub", PRODUCT_ID: ddlClass.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnTharaIndicExport_Click(object sender, EventArgs e)
        {
            try
            {
                Therapeutic_Indication(header: "Therapeutic Sub indication",id: ddlSubClass.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    
        private void Therapeutic_Masters(string header = null)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.Export_Data_SP(Action: "GET_TH_MASTER");
                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Therapeutic_class(string header = null)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.Export_Data_SP(Action: "GET_DATA");
                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Therapeutic_subClass(string header = null, string Action = null, string PRODUCT_ID = null)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.Export_Data_SP(Action: Action, PRODUCTID: PRODUCT_ID);
                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Therapeutic_Indication(string header = null, string Action = null, string id=null)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal.Export_Data_SP(Action: "GET_Indic", SUBCLASSID:id);
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