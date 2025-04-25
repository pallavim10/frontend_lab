using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTMS.CommonFunction;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class eTMF_MngGroups : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();

        CommonFunction.CommonFunction ComFun = new CommonFunction.CommonFunction();

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
                        GET_GROUP();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void INSERT_GROUP()
        {
            string Group_Name = txtGroup.Text;
            Group_Name = Group_Name.TrimStart();
            Group_Name = Group_Name.TrimEnd();

            if(chkEVENT.Checked || chkMILESTONE.Checked)
            {
                DataSet ds = dal_eTMF.eTMF_Group_SP(
                  ACTION: "INSERT_GROUP",
                  Group_Name: Group_Name,
                  Type_Event: chkEVENT.Checked,
                  Type_Milestone: chkMILESTONE.Checked
                  );

                CANCEL_GROUP();
                GET_GROUP();
                Response.Write("<script>alert('Group Defined successfully');</script>");
            }
            else
            {
                Response.Write("<script>alert('Please select group type.');</script>");
            }
            
        }

        protected void btnSubmitGroups_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_GROUP();
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnUpdateGroups_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_GROUP();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnCancelGroups_Click(object sender, EventArgs e)
        {
            CANCEL_GROUP();
        }

        private void CANCEL_GROUP()
        {
            try
            {
                txtGroup.Text = "";
                chkEVENT.Checked = false;
                chkMILESTONE.Checked = false;


                btnSubmitGroups.Visible = true;
                btnUpdateGroups.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_GROUP()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_Group_SP(ACTION: "GET_GROUP");
                grdGroups.DataSource = ds;
                grdGroups.DataBind();

                drpGroup.DataSource = ds;
                drpGroup.DataValueField = "ID";
                drpGroup.DataTextField = "Group_Name";
                drpGroup.DataBind();
                drpGroup.Items.Insert(0, new ListItem("--Select--", "0"));
                drpGroup_SelectedIndexChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void SELECT_GROUP(string ID)
        {
            ViewState["GROUPID"] = ID;
            try
            {
                DataSet ds = dal_eTMF.eTMF_Group_SP(
                ACTION: "SELECT_GROUP",
                ID: ID
                );
                txtGroup.Text = ds.Tables[0].Rows[0]["Group_Name"].ToString();

                if (ds.Tables[0].Rows[0]["Type_Event"].ToString() == "True")
                {
                    chkEVENT.Checked = true;
                }
                else
                {
                    chkEVENT.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["Type_Milestone"].ToString() == "True")
                {
                    chkMILESTONE.Checked = true;
                }
                else
                {
                    chkMILESTONE.Checked = false;
                }

                btnSubmitGroups.Visible = false;
                btnUpdateGroups.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        private void DELETE_GROUP(string ID)
        {
            try
            {
                dal_eTMF.eTMF_Group_SP(
                ACTION: "DELETE_GROUP",
                ID: ID
                    );

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Defined group deleted successfully.');  window.location.href = 'eTMF_MngGroups.aspx';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void UPDATE_GROUP()
        {
            try
            {
                string Group_Name = txtGroup.Text;
                Group_Name = Group_Name.TrimStart();
                Group_Name = Group_Name.TrimEnd();

                if (chkEVENT.Checked || chkMILESTONE.Checked)
                {
                    dal_eTMF.eTMF_Group_SP(
                        ACTION: "UPDATE_GROUP",
                        ID: ViewState["GROUPID"].ToString(),
                        Group_Name: Group_Name,
                        Type_Event: chkEVENT.Checked,
                        Type_Milestone: chkMILESTONE.Checked
                    );

                    CANCEL_GROUP();
                    GET_GROUP();

                    Response.Write("<script>alert('Defined group updated successfully.');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select group type.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grdGroups_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "EditGroup")
                {
                    SELECT_GROUP(e.CommandArgument.ToString());
                }
                else if (e.CommandName.ToString() == "DeleteGroup")
                {
                    DELETE_GROUP(e.CommandArgument.ToString());
                    GET_GROUP();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpGroup.SelectedIndex != 0)
                {
                    divOthers.Visible = true;

                    GET_NewZones();
                    GET_AddedZones();

                    GET_NewSections();
                    GET_AddedSections();

                    GET_NewArtifacts();
                    GET_AddedArtifacts();

                    //GET_NewDocs();
                    //GET_AddedDocs();

                    //GET_NewFiles();
                    //GET_AddedFiles();
                }
                else
                {
                    divOthers.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddZones_Click(object sender, EventArgs e)
        {
            try
            {
                AddZones();
                GET_NewZones();
                GET_AddedZones();
                GET_NewSections();
                GET_AddedSections();
                GET_NewArtifacts();
                GET_AddedArtifacts();
                //GET_NewDocs();
                //GET_AddedDocs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveZones_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveZones();
                GET_NewZones();
                GET_AddedZones();
                GET_NewSections();
                GET_AddedSections();
                GET_NewArtifacts();
                GET_AddedArtifacts();
                //GET_NewDocs();
                //GET_AddedDocs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_NewZones()
        {
            try
            {

                DataSet ds = dal_eTMF.eTMF_Group_SP(ACTION: "GET_NewZones", GroupID: drpGroup.SelectedValue);
                gvNewZones.DataSource = ds;
                gvNewZones.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AddedZones()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_Group_SP(ACTION: "GET_AddedZones", GroupID: drpGroup.SelectedValue);
                gvAddedZones.DataSource = ds;
                gvAddedZones.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void AddZones()
        {
            try
            {
                for (int i = 0; i < gvNewZones.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)gvNewZones.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblID = (Label)gvNewZones.Rows[i].FindControl("lblID");

                    if (ChAction.Checked)
                    {
                        DataSet ds = dal_eTMF.eTMF_Group_SP(ACTION: "AddZones", GroupID: drpGroup.SelectedValue, ZoneID: lblID.Text);
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RemoveZones()
        {
            try
            {
                for (int i = 0; i < gvAddedZones.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)gvAddedZones.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblID = (Label)gvAddedZones.Rows[i].FindControl("lblID");

                    if (ChAction.Checked)
                    {



                        dal_eTMF.eTMF_Group_SP(ACTION: "RemoveZones", GroupID: drpGroup.SelectedValue, ZoneID: lblID.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_NewSections()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_Group_SP(ACTION: "GET_NewSections", GroupID: drpGroup.SelectedValue);
                gvNewSections.DataSource = ds;
                gvNewSections.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AddedSections()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_Group_SP(ACTION: "GET_AddedSections", GroupID: drpGroup.SelectedValue);
                gvAddedSections.DataSource = ds;
                gvAddedSections.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void AddSections()
        {
            try
            {
                for (int i = 0; i < gvNewSections.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)gvNewSections.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblID = (Label)gvNewSections.Rows[i].FindControl("lblID");

                    Label lblZoneID = (Label)gvNewSections.Rows[i].FindControl("lblZoneID");

                    if (ChAction.Checked)
                    {
                        DataSet ds = dal_eTMF.eTMF_Group_SP(ACTION: "AddSections", GroupID: drpGroup.SelectedValue, ZoneID: lblZoneID.Text, SectionId: lblID.Text);


                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RemoveSections()
        {
            try
            {
                for (int i = 0; i < gvAddedSections.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)gvAddedSections.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblID = (Label)gvAddedSections.Rows[i].FindControl("lblID");

                    Label lblZoneID = (Label)gvAddedSections.Rows[i].FindControl("lblZoneID");

                    if (ChAction.Checked)
                    {

                        dal_eTMF.eTMF_Group_SP(ACTION: "RemoveSections", GroupID: drpGroup.SelectedValue, ZoneID: lblZoneID.Text, SectionId: lblID.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_NewArtifacts()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_Group_SP(ACTION: "GET_NewArtifacts", GroupID: drpGroup.SelectedValue);
                gvNewArtifacts.DataSource = ds;
                gvNewArtifacts.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AddedArtifacts()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_Group_SP(ACTION: "GET_AddedArtifacts", GroupID: drpGroup.SelectedValue);
                gvAddedArtifacts.DataSource = ds;
                gvAddedArtifacts.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void AddArtifacts()
        {
            try
            {
                for (int i = 0; i < gvNewArtifacts.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)gvNewArtifacts.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblID = (Label)gvNewArtifacts.Rows[i].FindControl("lblID");

                    Label lblZoneID = (Label)gvNewArtifacts.Rows[i].FindControl("lblZoneID");

                    Label lblSectionID = (Label)gvNewArtifacts.Rows[i].FindControl("lblSectionID");

                    if (ChAction.Checked)
                    {
                        DataSet ds = dal_eTMF.eTMF_Group_SP(
                       ACTION: "AddArtifacts",
                       GroupID: drpGroup.SelectedValue,
                       ZoneID: lblZoneID.Text,
                       SectionId: lblSectionID.Text,
                       ArtifactId: lblID.Text
                       );

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RemoveArtifacts()
        {
            try
            {
                for (int i = 0; i < gvAddedArtifacts.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)gvAddedArtifacts.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblID = (Label)gvAddedArtifacts.Rows[i].FindControl("lblID");

                    Label lblZoneID = (Label)gvAddedArtifacts.Rows[i].FindControl("lblZoneID");

                    Label lblSectionID = (Label)gvAddedArtifacts.Rows[i].FindControl("lblSectionID");

                    if (ChAction.Checked)
                    {


                        dal_eTMF.eTMF_Group_SP(
                        ACTION: "RemoveArtifacts",
                        GroupID: drpGroup.SelectedValue,
                        ZoneID: lblZoneID.Text,
                        SectionId: lblSectionID.Text,
                        ArtifactId: lblID.Text
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddArtifacts_Click(object sender, EventArgs e)
        {
            AddArtifacts();
            GET_NewArtifacts();
            GET_AddedArtifacts();
            //GET_NewDocs();
            //GET_AddedDocs();
        }

        protected void lbtnRemoveArtifacts_Click(object sender, EventArgs e)
        {
            RemoveArtifacts();
            GET_NewArtifacts();
            GET_AddedArtifacts();
            //GET_NewDocs();
            //GET_AddedDocs();
        }

        protected void lbtnAddSections_Click(object sender, EventArgs e)
        {
            try
            {
                AddSections();
                GET_NewSections();
                GET_AddedSections();
                GET_NewArtifacts();
                GET_AddedArtifacts();
                //GET_NewDocs();
                //GET_AddedDocs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveSections_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveSections();
                GET_NewSections();
                GET_AddedSections();
                GET_NewArtifacts();
                GET_AddedArtifacts();
                //GET_NewDocs();
                //GET_AddedDocs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdGroups_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    HtmlControl iconEvent = (HtmlControl)e.Row.FindControl("iconEvent");
                    if (drv["Type_Event"].ToString() == "True")
                    {
                        iconEvent.Attributes.Add("class", "fa fa-check");
                        iconEvent.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconEvent.Attributes.Add("class", "fa fa-times");
                        iconEvent.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconMilestone = (HtmlControl)e.Row.FindControl("iconMilestone");
                    if (drv["Type_Milestone"].ToString() == "True")
                    {
                        iconMilestone.Attributes.Add("class", "fa fa-check");
                        iconMilestone.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconMilestone.Attributes.Add("class", "fa fa-times");
                        iconMilestone.Attributes.Add("style", "color: red;");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbnGroupsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "eTMF Group.xls";

                DataSet ds = dal_eTMF.eTMF_LOG_SP(
                     ACTION: "GROUP_AUDITTRAIL"
                     );
                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdGroups_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }
    }
}