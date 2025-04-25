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
    public partial class eTMF_DefineSnapshot : System.Web.UI.Page
    {
        DAL dal = new DAL();
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
                        GET_SNAPSHOT();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void CANCEL_SNAPSHOT()
        {
            try
            {
                txtSnapshot.Text = "";
                chkSPONSOR.Checked = false;
                chkSITE.Checked = false;
                chkETMF.Checked = false;

                btnSubmitSnapshot.Visible = true;
                btnUpdateSnapshot.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void INSERT_SNAPSHOT()
        {
            try
            {
                string Snapshot = txtSnapshot.Text;
                Snapshot = Snapshot.TrimStart();
                Snapshot = Snapshot.TrimEnd();

                DataSet ds = dal.eTMF_Snapshot_SP(
               ACTION: "INSERT_SNAPSHOT",
               Snapshot: Snapshot,
               Sponsor: chkSPONSOR.Checked,
               Site: chkSITE.Checked,
               eTMF: chkETMF.Checked
                   );
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void UPDATE_SNAPSHOT()
        {
            try
            {
                string Snapshot = txtSnapshot.Text;
                 Snapshot = Snapshot.TrimStart();
                 Snapshot = Snapshot.TrimEnd();


                dal.eTMF_Snapshot_SP(
                ACTION: "UPDATE_SNAPSHOT",
                ID: ViewState["SnapID"].ToString(),
                Snapshot: Snapshot,
                Sponsor: chkSPONSOR.Checked,
                Site: chkSITE.Checked,
                eTMF: chkETMF.Checked
                    );
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void DELETE_SNAPSHOT(string ID)
        {
            try
            {
                

                dal.eTMF_Snapshot_SP(
                ACTION: "DELETE_SNAPSHOT",
                ID: ID
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void SELECT_SNAPSHOT(string ID)
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(
                ACTION: "SELECT_SNAPSHOT",
                ID: ID
                );

                ViewState["SnapID"] = ID;

                txtSnapshot.Text = ds.Tables[0].Rows[0]["Snapshot"].ToString();

                if (ds.Tables[0].Rows[0]["Site"].ToString() == "True")
                {
                    chkSITE.Checked = true;
                }
                else
                {
                    chkSITE.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["Sponsor"].ToString() == "True")
                {
                    chkSPONSOR.Checked = true;
                }
                else
                {
                    chkSPONSOR.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["eTMF"].ToString() == "True")
                {
                    chkETMF.Checked = true;
                }
                else
                {
                    chkETMF.Checked = false;
                }

                btnSubmitSnapshot.Visible = false;
                btnUpdateSnapshot.Visible = true;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_SNAPSHOT()
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_SNAPSHOT");
                grdSnapshot.DataSource = ds;
                grdSnapshot.DataBind();

                drpSnapshot.DataSource = ds;
                drpSnapshot.DataValueField = "ID";
                drpSnapshot.DataTextField = "Snapshot";
                drpSnapshot.DataBind();
                drpSnapshot.Items.Insert(0, new ListItem("--Select--", "0"));
                drpSnapshot_SelectedIndexChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnSubmitSnapshot_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_SNAPSHOT();
                CANCEL_SNAPSHOT();
                GET_SNAPSHOT();

                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("eTMF");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateSnapshot_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_SNAPSHOT();
                CANCEL_SNAPSHOT();
                GET_SNAPSHOT();

                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("eTMF");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelSnapshot_Click(object sender, EventArgs e)
        {
            try
            {
                CANCEL_SNAPSHOT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSnapshot_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "EditSnapshot")
                {
                    SELECT_SNAPSHOT(e.CommandArgument.ToString());
                }
                else if (e.CommandName.ToString() == "DeleteSnapshot")
                {
                    DELETE_SNAPSHOT(e.CommandArgument.ToString());
                    GET_SNAPSHOT();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSnapshot_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpSnapshot.SelectedIndex != 0)
                {
                    divOthers.Visible = true;

                    GET_NewZones();
                    GET_AddedZones();

                    GET_NewSections();
                    GET_AddedSections();

                    GET_NewArtifacts();
                    GET_AddedArtifacts();

                    GET_NewDocs();
                    GET_AddedDocs();

                    GET_NewFiles();
                    GET_AddedFiles();
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



        //For Zones

        private void GET_NewZones()
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_NewZones", SnapId: drpSnapshot.SelectedValue);
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
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_AddedZones", SnapId: drpSnapshot.SelectedValue);
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
                        DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "AddZones", SnapId: drpSnapshot.SelectedValue, ZoneID: lblID.Text);

                       
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

                      

                        dal.eTMF_Snapshot_SP(ACTION: "RemoveZones", SnapId: drpSnapshot.SelectedValue, ZoneID: lblID.Text);
                    }
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
                GET_NewDocs();
                GET_AddedDocs();
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
                GET_NewDocs();
                GET_AddedDocs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        //For Sections

        private void GET_NewSections()
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_NewSections", SnapId: drpSnapshot.SelectedValue);
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
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_AddedSections", SnapId: drpSnapshot.SelectedValue);
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
                        DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "AddSections", SnapId: drpSnapshot.SelectedValue, ZoneID: lblZoneID.Text, SectionId: lblID.Text);

                        
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
                        
                        dal.eTMF_Snapshot_SP(ACTION: "RemoveSections", SnapId: drpSnapshot.SelectedValue, ZoneID: lblZoneID.Text, SectionId: lblID.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
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
                GET_NewDocs();
                GET_AddedDocs();
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
                GET_NewDocs();
                GET_AddedDocs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        //For Artifacts

        private void GET_NewArtifacts()
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_NewArtifacts", SnapId: drpSnapshot.SelectedValue);
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
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_AddedArtifacts", SnapId: drpSnapshot.SelectedValue);
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
                        DataSet ds = dal.eTMF_Snapshot_SP(
                       ACTION: "AddArtifacts",
                       SnapId: drpSnapshot.SelectedValue,
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
                        

                        dal.eTMF_Snapshot_SP(
                        ACTION: "RemoveArtifacts",
                        SnapId: drpSnapshot.SelectedValue,
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
            try
            {
                AddArtifacts();
                GET_NewArtifacts();
                GET_AddedArtifacts();
                GET_NewDocs();
                GET_AddedDocs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveArtifacts_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveArtifacts();
                GET_NewArtifacts();
                GET_AddedArtifacts();
                GET_NewDocs();
                GET_AddedDocs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        //For Documents

        private void GET_NewDocs()
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_NewDocs", SnapId: drpSnapshot.SelectedValue);
                gvNewDocuments.DataSource = ds;
                gvNewDocuments.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AddedDocs()
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_AddedDocs", SnapId: drpSnapshot.SelectedValue);
                gvAddedDocuments.DataSource = ds;
                gvAddedDocuments.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void AddDocs()
        {
            try
            {
                for (int i = 0; i < gvNewDocuments.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)gvNewDocuments.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblID = (Label)gvNewDocuments.Rows[i].FindControl("lblID");

                    if (ChAction.Checked)
                    {
                        DataSet ds = dal.eTMF_Snapshot_SP(
                         ACTION: "AddDocs",
                         SnapId: drpSnapshot.SelectedValue,
                         DocId: lblID.Text
                         );
                       
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RemoveDocs()
        {
            try
            {
                for (int i = 0; i < gvAddedDocuments.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)gvAddedDocuments.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblID = (Label)gvAddedDocuments.Rows[i].FindControl("lblID");

                    if (ChAction.Checked)
                    {
                        

                        dal.eTMF_Snapshot_SP(
                        ACTION: "RemoveDocs",
                        SnapId: drpSnapshot.SelectedValue,
                        DocId: lblID.Text
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                AddDocs();
                GET_NewDocs();
                GET_AddedDocs();

                GET_NewFiles();
                GET_AddedFiles();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveDocs();
                GET_NewDocs();
                GET_AddedDocs();

                GET_NewFiles();
                GET_AddedFiles();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //For Files

        private void GET_NewFiles()
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_NewFiles", SnapId: drpSnapshot.SelectedValue);
                grdFiles.DataSource = ds;
                grdFiles.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AddedFiles()
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_AddedFiles", SnapId: drpSnapshot.SelectedValue);
                grdAddedFiles.DataSource = ds;
                grdAddedFiles.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void AddFiles()
        {
            try
            {
                for (int i = 0; i < grdFiles.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)grdFiles.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblFILEID = (Label)grdFiles.Rows[i].FindControl("lblID");
                    Label lblDOCID = (Label)grdFiles.Rows[i].FindControl("DOCID");

                    if (ChAction.Checked)
                    {
                        DataSet ds = dal.eTMF_Snapshot_SP(
                       ACTION: "AddFiles",
                       SnapId: drpSnapshot.SelectedValue,
                       DocId: lblDOCID.Text,
                       FileId: lblFILEID.Text
                       );

                        

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RemoveFiles()
        {
            try
            {
                for (int i = 0; i < grdAddedFiles.Rows.Count; i++)
                {
                    CheckBox ChAction = (CheckBox)grdAddedFiles.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblFILEID = (Label)grdAddedFiles.Rows[i].FindControl("lblID");
                    Label lblDOCID = (Label)grdAddedFiles.Rows[i].FindControl("DOCID");

                    if (ChAction.Checked)
                    {
                       
                        dal.eTMF_Snapshot_SP(
                        ACTION: "RemoveFiles",
                        SnapId: drpSnapshot.SelectedValue,
                        DocId: lblDOCID.Text,
                        FileId: lblFILEID.Text
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddFiles_Click(object sender, EventArgs e)
        {
            try
            {
                AddFiles();
                GET_NewFiles();
                GET_AddedFiles();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveFiles_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveFiles();
                GET_NewFiles();
                GET_AddedFiles();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}