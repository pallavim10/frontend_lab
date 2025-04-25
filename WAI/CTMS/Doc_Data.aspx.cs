using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class Doc_Data : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Doc();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_Doc()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_PROJECT_Doc", ProjectID: Session["PROJECTID"].ToString());
                drpPlan.DataSource = ds.Tables[0];
                drpPlan.DataValueField = "DocID";
                drpPlan.DataTextField = "DocName";
                drpPlan.DataBind();
                drpPlan.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_Sec()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_PROJECT_Sec", ProjectID: Session["PROJECTID"].ToString(), DocID: drpPlan.SelectedValue);
                drpSection.DataSource = ds.Tables[0];
                drpSection.DataValueField = "SecID";
                drpSection.DataTextField = "SectionName";
                drpSection.DataBind();
                drpSection.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Sec();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetData()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_PROJECT_DATA", ProjectID: Session["PROJECTID"].ToString(), SecID: drpSection.SelectedValue, DocID: drpPlan.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtData.Text = ds.Tables[0].Rows[0]["DATA"].ToString();
                    hfOldData.Value = ds.Tables[0].Rows[0]["DATA"].ToString();
                }
                else
                {
                    txtData.Text = "";
                    hfOldData.Value = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dal.Documents_SP(Action: "INSERT_DATA", DocID: drpPlan.SelectedValue, SecID: drpSection.SelectedValue, Data: txtData.Text, ProjectID: Session["PROJECTID"].ToString(), OldData: hfOldData.Value, UserID: Session["USER_ID"].ToString());
                Response.Write("<script> alert('Record Updated successfully.');</script>");
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
                Response.Redirect("Doc_Data.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}