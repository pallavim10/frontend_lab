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
    public partial class HOMEPAGE_DEFINE_TABS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        public string AnsColorValue = "#000000";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GETTABSDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETTABSDATA()
        {
            try
            {
                DataSet ds = dal.Dashboard_SP(Action: "GET_HOMEPAGE_TABS", Project_ID: Session["PROJECTID"].ToString(), User_ID: Session["User_ID"].ToString());

                grdTabs.DataSource = ds.Tables[0];
                grdTabs.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.Dashboard_SP(Action: "INSERT_TABS",
                User_ID: Session["User_ID"].ToString(),
                Project_ID: Session["PROJECTID"].ToString(),
                Type: txtTabName.Text,
                Chart_ID: txtSEQNO.Text
                );

                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                txtTabName.Text = "";
                txtSEQNO.Text = "";
                GETTABSDATA();
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
                DataSet ds = dal.Dashboard_SP(Action: "UPDATE_TABS",
                Type: txtTabName.Text,
                ID: ViewState["ID"].ToString(),
                Chart_ID: txtSEQNO.Text
                );

                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                txtTabName.Text = "";
                txtSEQNO.Text = "";
                GETTABSDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            txtTabName.Text = "";
            txtSEQNO.Text = "";
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void grdTabs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string ID = dr["ID"].ToString();
                    string TABSNAME = dr["TABSNAME"].ToString();
                    LinkButton lbtnupdate = (LinkButton)e.Row.FindControl("lbtnupdate");
                    LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");

                    if (ID == "1" || ID == "2" || ID == "3" || ID == "4" || ID == "5")
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }

        }

        protected void grdTabs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                ViewState["ID"] = id;

                if (e.CommandName == "EditField")
                {
                    DataSet ds = dal.Dashboard_SP(Action: "GET_TABS_BYID", ID: id);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtTabName.Text = ds.Tables[0].Rows[0]["TABSNAME"].ToString();
                        txtSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                        AnsColorValue = ds.Tables[0].Rows[0]["TABSCOLOR"].ToString();
                        hfAnsColor.Value = ds.Tables[0].Rows[0]["TABSCOLOR"].ToString();
                    }

                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (e.CommandName == "DeleteField")
                {
                    DataSet ds = dal.Dashboard_SP(Action: "DELETE_TABS", ID: id);
                    GETTABSDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}