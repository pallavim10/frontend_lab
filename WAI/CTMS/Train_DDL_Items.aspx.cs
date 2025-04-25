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
    public partial class Train_DDL_Items : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ID = Request.QueryString["ID"].ToString();
                get_Items();
            }
        }

        protected void btnAddPlan_Click(object sender, EventArgs e)
        {
            insert_Items();
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string id = e.CommandArgument.ToString();
            delete_Items(id);
        }

        private void insert_Items()
        {
            try
            {
                if (Request.QueryString["Type"] != null)
                {
                    dal.Train_Verification_SP(Action: "insert_ddl_Site", Items: txtItem.Text, QueNo: Request.QueryString["ID"].ToString());
                }
                else
                {
                    dal.Train_Verification_SP(Action: "insert_ddl", Items: txtItem.Text, QueNo: Request.QueryString["ID"].ToString());
                }
                txtItem.Text = "";
                get_Items();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Items()
        {
            try
            {
                if (Request.QueryString["Type"] != null)
                {
                    DataSet ds = dal.Train_Verification_SP(Action: "get_ddl_Site", QueNo: Request.QueryString["ID"].ToString());
                    gvItems.DataSource = ds.Tables[0];
                    gvItems.DataBind();
                }
                else
                {
                    DataSet ds = dal.Train_Verification_SP(Action: "get_ddl", QueNo: Request.QueryString["ID"].ToString());
                    gvItems.DataSource = ds.Tables[0];
                    gvItems.DataBind();                
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Items(string id)
        {
            try
            {
                if (Request.QueryString["Type"] != null)
                {
                    dal.Train_Verification_SP(Action: "delete_ddl_Site", ID: id);
                }
                else
                {
                    dal.Train_Verification_SP(Action: "delete_ddl", ID: id);
                }
                get_Items();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}