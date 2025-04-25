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
    public partial class Doc_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Plan();
                    btnupdatePlan.Visible = false;
                    btnupdateSection.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitPlan_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Plan();
                txtPlan.Text = "";
                txtSeq.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdatePlan_Click(object sender, EventArgs e)
        {
            try
            {
                update_Plan();
                txtPlan.Text = "";
                txtSeq.Text = "";
                btnupdatePlan.Visible = false;
                btnSubmitPlan.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelPlan_Click(object sender, EventArgs e)
        {
            try
            {
                txtPlan.Text = "";
                txtSeq.Text = "";
                btnupdatePlan.Visible = false;
                btnSubmitPlan.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Plan()
        {
            try
            {
                dal.Documents_SP(Action: "INSERT_DOC", DocName: txtPlan.Text, SEQNO: txtSeq.Text);
                bind_Plan();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Plan()
        {
            try
            {
                dal.Documents_SP(Action: "UPDATE_DOC", DocName: txtPlan.Text, ID: Session["PlanID"].ToString(), SEQNO: txtSeq.Text);
                bind_Plan();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Plan()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_DOC");
                gvPlan.DataSource = ds.Tables[0];
                gvPlan.DataBind();

                ddlPlan.DataSource = ds.Tables[0];
                ddlPlan.DataValueField = "ID";
                ddlPlan.DataTextField = "DocName";
                ddlPlan.DataBind();
                ddlPlan.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_GVPlan()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_DOC");
                gvPlan.DataSource = ds.Tables[0];
                gvPlan.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Plan(string ID)
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "SELECT_DOC", ID: ID);
                txtPlan.Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                txtSeq.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Plan(string ID)
        {
            try
            {
                dal.Documents_SP(Action: "DELETE_DOC", ID: ID);
                bind_Plan();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Section()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_SEC", ID: ddlPlan.SelectedValue);
                gvSection.DataSource = ds.Tables[0];
                gvSection.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Section()
        {
            try
            {
                dal.Documents_SP(Action: "INSERT_SEC", ID: ddlPlan.SelectedValue, SectionName: txtSection.Text, SEQNO: txtSubSeq.Text);
                bind_GVPlan();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Section()
        {
            try
            {
                dal.Documents_SP(Action: "UPDATE_SEC", ID: Session["SectionID"].ToString(), DocID: ddlPlan.SelectedValue, SectionName: txtSection.Text, SEQNO: txtSubSeq.Text);
                bind_GVPlan();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Section(string ID)
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "SELECT_SEC", ID: ID);
                txtSection.Text = ds.Tables[0].Rows[0]["SectionName"].ToString();
                txtSubSeq.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                btnupdateSection.Visible = true;
                btnsubmitSection.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Section(string ID)
        {
            try
            {
                dal.Documents_SP(Action: "DELETE_SEC", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Section();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitSection_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Section();
                txtSection.Text = "";
                txtSubSeq.Text = "";
                bind_Section();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateSection_Click(object sender, EventArgs e)
        {
            try
            {
                update_Section();
                txtSection.Text = "";
                txtSubSeq.Text = "";
                btnupdateSection.Visible = false;
                btnsubmitSection.Visible = true;
                bind_Section();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelSection_Click(object sender, EventArgs e)
        {
            try
            {
                txtSection.Text = "";
                txtSubSeq.Text = "";
                btnupdateSection.Visible = false;
                btnsubmitSection.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvPlan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeletePlan = (e.Row.FindControl("lbtndeletePlan") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeletePlan.Visible = false;
                    }
                    else
                    {
                        lbtndeletePlan.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvPlan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["PlanID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Plan(id);
                    btnupdatePlan.Visible = true;
                    btnSubmitPlan.Visible = false;
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Plan(id);
                    bind_Plan();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSection_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["SectionID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Section(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Section(id);
                    bind_Section();
                    bind_GVPlan();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}