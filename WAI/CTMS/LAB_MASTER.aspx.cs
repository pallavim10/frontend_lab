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
    public partial class LAB_MASTER : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillINV();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Clear_Lab()
        {
            try
            {
                txtLabID.Text = "";
                txtLabName.Text = "";
                btnsubmitLab.Visible = true;
                btnupdateLab.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_Lab()
        {
            try
            {
                DataSet ds = dal.LAB_MASTER_SP(Action: "GET_Lab", INVID: drpInvid.SelectedValue);
                grdLab.DataSource = ds.Tables[0];
                grdLab.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void INSERT_Lab()
        {
            try
            {
                dal.LAB_MASTER_SP(Action: "INSERT_Lab", INVID: drpInvid.SelectedValue, Lab_ID: txtLabID.Text, Lab_Name: txtLabName.Text);
                Clear_Lab();
                GET_Lab();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void UPDATE_Lab()
        {
            try
            {
                dal.LAB_MASTER_SP(Action: "UPDATE_Lab", Lab_ID: txtLabID.Text, Lab_Name: txtLabName.Text, ID: Session["LAB_ID"].ToString());
                Clear_Lab();
                GET_Lab();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DELETE_Lab(string ID)
        {
            try
            {
                dal.LAB_MASTER_SP(Action: "DELETE_Lab", ID: ID);
                Clear_Lab();
                GET_Lab();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SELECT_Lab(string ID)
        {
            try
            {
                Session["LAB_ID"] = ID;
                DataSet ds = dal.LAB_MASTER_SP(Action: "SELECT_Lab", ID: ID);
                txtLabID.Text = ds.Tables[0].Rows[0]["Lab_ID"].ToString();
                txtLabName.Text = ds.Tables[0].Rows[0]["Lab_Name"].ToString();
                btnsubmitLab.Visible = false;
                btnupdateLab.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GetSiteID(
            Action: "INVID",
            PROJECTID:Session["PROJECTID"].ToString(),         
            User_ID: Session["User_ID"].ToString()
            );
            drpInvid.DataSource = ds.Tables[0];
            drpInvid.DataValueField = "INVNAME";
            drpInvid.DataBind();
            drpInvid.Items.Insert(0, new ListItem("--Select INVID--", "0"));
        }








        protected void drpInvid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_Lab();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitLab_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_Lab();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateLab_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_Lab();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelLab_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Lab();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdVisit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();

                if (e.CommandName == "EditLab")
                {
                    SELECT_Lab(ID);
                }
                else if (e.CommandName == "DeleteLab")
                {
                    DELETE_Lab(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}