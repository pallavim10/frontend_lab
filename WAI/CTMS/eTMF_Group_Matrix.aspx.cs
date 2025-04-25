using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_Group_Matrix : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["prevURL"] = Request.Url.PathAndQuery.ToString();

                        Get_GROUP();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_GROUP()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_eTMF.eTMF_GROUP_MATRIX_SP(Action: "Get_GROUP");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    ddlGroup.DataSource = ds.Tables[0];
                    ddlGroup.DataValueField = "ID";
                    ddlGroup.DataTextField = "Group_name";
                    ddlGroup.DataBind();
                    ddlGroup.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlGroup.DataSource = null;
                    ddlGroup.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_ZONES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            BIND_SECTIONS();
        }

        private void BIND_ZONES()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal_eTMF.eTMF_GROUP_MATRIX_SP(Action: "GET_ZONES", GroupID: ddlGroup.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlZone.DataSource = ds.Tables[0];
                    ddlZone.DataValueField = "ZoneID";
                    ddlZone.DataTextField = "Zones";
                    ddlZone.DataBind();
                    ddlZone.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlZone.DataSource = null;
                    ddlZone.DataBind();

                    ddlSections.DataSource = null;
                    ddlSections.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_SECTIONS()
        {
            DataSet ds = new DataSet();
            ds = dal_eTMF.eTMF_GROUP_MATRIX_SP(Action: "GET_SECTIONS", GroupID: ddlGroup.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSections.DataSource = ds.Tables[0];
                ddlSections.DataValueField = "SectionID";
                ddlSections.DataTextField = "Sections";
                ddlSections.DataBind();
                ddlSections.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlSections.DataSource = null;
                ddlSections.DataBind();
            }
        }

        private void BIND_ARTIFACT()
        {
            int SubFolderDocs = 0;
            DataSet ds = new DataSet();

            ds = dal_eTMF.eTMF_GROUP_MATRIX_SP(Action: "GET_ARTIFACT", ZoneID: ddlZone.SelectedValue, SectionID: ddlSections.SelectedValue, GroupID: ddlGroup.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvArtifact.DataSource = ds.Tables[0];
                gvArtifact.DataBind();
                lblCount.Text = SubFolderDocs.ToString();
                DATAVIEW.Visible = true;
            }
            else
            {
                gvArtifact.DataSource = null;
                gvArtifact.DataBind();
                lblCount.Text = "0";
                DATAVIEW.Visible = true;
            }
        }

        protected void ddlSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            BIND_ARTIFACT();
        }

        protected void gvArtifact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int ArtifactDocs = 0;

                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string MainRefNo = drv["MainRefNo"].ToString();
                    string DocTypeId = drv["DocTypeId"].ToString();

                    GridView gvDocs = e.Row.FindControl("gvDocs") as GridView;
                    DataSet ds = dal_eTMF.eTMF_GROUP_MATRIX_SP(Action: "Get_GVBY_GROUPDATA", RefNo: MainRefNo, DocTypeId: DocTypeId);
                    gvDocs.DataSource = ds.Tables[0];
                    gvDocs.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = ArtifactDocs.ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvDocs.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            Get_GROUP();

            ddlZone.DataSource = null;
            ddlZone.DataBind();

            ddlSections.DataSource = null;
            ddlSections.DataBind();

            DATAVIEW.Visible = false;



        }

    }

}