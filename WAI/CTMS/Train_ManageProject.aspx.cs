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
    public partial class Train_ManageProject : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    get_Section();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Section()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_Section");

                ddlSec.DataSource = ds.Tables[0];
                ddlSec.DataValueField = "ID";
                ddlSec.DataTextField = "Section";
                ddlSec.DataBind();
                ddlSec.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_SubSection()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_Project_Trains", Section_ID: ddlSec.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvSubSec.DataSource = ds.Tables[0];
                    gvSubSec.DataBind();
                    btnSubmit.Visible = true;
                }
                else
                {
                    gvSubSec.DataSource = null;
                    gvSubSec.DataBind();
                    btnSubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_SubSection();
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
                string Internal = "";
                string Site = "";
                for (int i = 0; i < gvSubSec.Rows.Count; i++)
                {
                    Label lbl_SectionID = (Label)gvSubSec.Rows[i].FindControl("lbl_SectionID");
                    Label lbl_SubSectionID = (Label)gvSubSec.Rows[i].FindControl("lbl_SubSectionID");
                    CheckBox chkInternal = (CheckBox)gvSubSec.Rows[i].FindControl("chkInternal");
                    CheckBox chkSite = (CheckBox)gvSubSec.Rows[i].FindControl("chkSite");

                    if (chkInternal.Checked)
                    {
                        Internal = "1";
                    }
                    else
                    {
                        Internal = "";
                    }

                    if (chkSite.Checked)
                    {
                        Site = "1";
                    }
                    else
                    {
                        Site = "";
                    }

                    dal.Training_SP(
                    Action: "update_Project_Trains",
                    Project_ID: Session["PROJECTID"].ToString(),
                    Section_ID: lbl_SectionID.Text,
                    SubSection_ID: lbl_SubSectionID.Text,
                    Site: Site,
                    Internal: Internal
                        );
                }
                Response.Write("<script> alert('Record Updated successfully.')</script>");
                get_SubSection();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSubSec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Internal = drv["Internal"].ToString();
                    string Site = drv["Site"].ToString();
                    
                    CheckBox chkInternal = (e.Row.FindControl("chkInternal") as CheckBox);
                    if (Internal == "True")
                    {
                        chkInternal.Checked = true;
                    }
                    else
                    {
                        chkInternal.Checked = false;
                    }
                    CheckBox chkSite = (e.Row.FindControl("chkSite") as CheckBox);
                    if (Site == "True")
                    {
                        chkSite.Checked = true;
                    }
                    else
                    {
                        chkSite.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}