using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class CTMS_EssentialDoc_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    bind_DocType();
                    bind_Folder();
                    
                    btnupdateFolder.Visible = false;
                    btnupdateSubFolder.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitDocType_Click(object sender, EventArgs e)
        {
            try
            {
                insert_DocType();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateDocType_Click(object sender, EventArgs e)
        {
            try
            {
                update_DocType();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelDocType_Click(object sender, EventArgs e)
        {
            try
            {
                btnupdateDocType.Visible = true;
                btnSubmitDocType.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_DocType()
        {
            try
            {
                string DocType = txtDocType.Text;
                DocType = DocType.TrimStart();
                DocType = DocType.TrimEnd();
                DataSet ds = dal_eTMF.eTMF_SET_SP(
                ACTION: "insert_DocType",
                DocType: DocType,
                User: Session["User_ID"].ToString(),
                Project_ID: Session["PROJECTID"].ToString()
                );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('TMF model added Successfully.')", true);

                bind_DocType();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_DocType()
        {
            try
            {
                string DocType = txtDocType.Text;
                DocType = DocType.TrimStart();
                DocType = DocType.TrimEnd();
                DataSet ds = dal_eTMF.eTMF_SET_SP(
                ACTION: "update_DocType",
                DocType: DocType,
                User: Session["User_ID"].ToString(),
                Project_ID: Session["PROJECTID"].ToString()
                );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('TMF module updated successfully.')", true);

                bind_DocType();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_DocType()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_DocType");
                
                if(ds.Tables.Count >0 && ds.Tables[0].Rows.Count >0)
                {
                    txtDocType.Text = ds.Tables[0].Rows[0]["DocType"].ToString();
                    btnupdateDocType.Visible = true;
                    btnSubmitDocType.Visible = false;
                }
                else
                {
                    txtDocType.Text = "";
                    btnupdateDocType.Visible = false;
                    btnSubmitDocType.Visible = true;
                }

                ddlDocType.DataSource = ds.Tables[0];
                ddlDocType.DataValueField = "ID";
                ddlDocType.DataTextField = "DocType";
                ddlDocType.DataBind();
                ddlDocType.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitFolder_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Folder();
                txtFolder.Text = "";
                txtSeq.Text = "";
                txtFolderRef.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateFolder_Click(object sender, EventArgs e)
        {
            try
            {
                update_Folder();
                txtFolder.Text = "";
                txtSeq.Text = "";
                txtFolderRef.Text = "";
                btnupdateFolder.Visible = false;
                btnSubmitFolder.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelFolder_Click(object sender, EventArgs e)
        {
            try
            {
                txtFolder.Text = "";
                txtSeq.Text = "";
                txtFolderRef.Text = "";
                btnupdateFolder.Visible = false;
                btnSubmitFolder.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Folder();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Folder()
        {
            try
            {
                string Folder = txtFolder.Text;
                Folder = Folder.TrimStart();
                Folder = Folder.TrimEnd();

                DataSet ds = dal_eTMF.eTMF_SET_SP(
                    ACTION: "insert_Folder",
                    DocType: ddlDocType.SelectedValue,
                    Folder: Folder,
                    SEQNO: txtSeq.Text,
                    RefNo: txtFolderRef.Text
                    );

                string dsPID = ds.Tables[0].Rows[0]["ID"].ToString();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Zone added successfully.')", true);
                bind_Folder();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Folder()
        {
            try
            {
                string Folder = txtFolder.Text;
                Folder = Folder.TrimStart();
                Folder = Folder.TrimEnd();
                dal_eTMF.eTMF_SET_SP(
                    ACTION: "update_Folder",
                    DocType: ddlDocType.SelectedValue,
                    Folder: Folder,
                    ID: Session["FOLDERID"].ToString(),
                    SEQNO: txtSeq.Text, RefNo: txtFolderRef.Text);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Zone updated successfully.')", true);
                bind_Folder();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Folder()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_Folder", DocType: ddlDocType.SelectedValue);
                gvFolder.DataSource = ds.Tables[0];
                gvFolder.DataBind();

                ddlFolder.DataSource = ds.Tables[0];
                ddlFolder.DataValueField = "ID";
                ddlFolder.DataTextField = "Folder";
                ddlFolder.DataBind();
                ddlFolder.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Folder(string ID)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "select_Folder", ID: ID);
                ddlDocType.SelectedValue = ds.Tables[0].Rows[0]["DocType"].ToString();
                txtFolder.Text = ds.Tables[0].Rows[0]["Folder"].ToString();
                txtSeq.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtFolderRef.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Folder(string ID)
        {
            try
            {

                dal_eTMF.eTMF_SET_SP(ACTION: "delete_Folder", ID: ID);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Zone deleted successfully.')", true);
                bind_Folder();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_SubFolder()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_SubFolder", ID: ddlFolder.SelectedValue);
                gvSubFolder.DataSource = ds.Tables[0];
                gvSubFolder.DataBind();

                ddlSection.DataSource = ds.Tables[0];
                ddlSection.DataValueField = "ID";
                ddlSection.DataTextField = "Sub_Folder_Name";
                ddlSection.DataBind();
                ddlSection.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_GVSubFolder()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_SubFolder", ID: ddlFolder.SelectedValue);
                gvSubFolder.DataSource = ds.Tables[0];
                gvSubFolder.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_SubFolder()
        {
            try
            {
                string subFolder = txtSubFolder.Text;
                subFolder = subFolder.TrimStart();
                subFolder = subFolder.TrimEnd();
                DataSet ds = dal_eTMF.eTMF_SET_SP(
                    ACTION: "insert_SubFolder",
                    ID: ddlFolder.SelectedValue,
                    Folder: subFolder,
                    SEQNO: txtSubSeq.Text,
                    RefNo: txtSubRef.Text
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Section Added successfully.')", true);

                bind_GVSubFolder();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_SubFolder()
        {
            try
            {
                string subFolder = txtSubFolder.Text;
                subFolder = subFolder.TrimStart();
                subFolder = subFolder.TrimEnd();

                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "update_SubFolder", ID: Session["SUBFOLDERID"].ToString(), Folder: subFolder, SEQNO: txtSubSeq.Text, RefNo: txtSubRef.Text);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Section updated successfully.')", true);
                bind_GVSubFolder();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_SubFolder(string ID)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "select_SubFolder", ID: ID);
                txtSubFolder.Text = ds.Tables[0].Rows[0]["Sub_Folder_Name"].ToString();
                txtSubSeq.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtSubRef.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();
                btnupdateSubFolder.Visible = true;
                btnsubmitSubFolder.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_SubFolder(string ID)
        {
            try
            {
                dal_eTMF.eTMF_SET_SP(ACTION: "delete_SubFolder", ID: ID);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Section deleted successfully.')", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_SubFolder();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitSubFolder_Click(object sender, EventArgs e)
        {
            try
            {
                insert_SubFolder();
                txtSubFolder.Text = "";
                txtSubSeq.Text = "";
                txtSubRef.Text = "";
                bind_SubFolder();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateSubFolder_Click(object sender, EventArgs e)
        {
            try
            {
                update_SubFolder();
                txtSubFolder.Text = "";
                txtSubSeq.Text = "";
                txtSubRef.Text = "";
                btnupdateSubFolder.Visible = false;
                btnsubmitSubFolder.Visible = true;
                bind_SubFolder();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelSubFolder_Click(object sender, EventArgs e)
        {
            try
            {
                txtSubFolder.Text = "";
                txtSubSeq.Text = "";
                txtSubRef.Text = "";
                btnupdateSubFolder.Visible = false;
                btnsubmitSubFolder.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvFolder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteFolder = (e.Row.FindControl("lbtndeleteFolder") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteFolder.Visible = false;
                    }
                    else
                    {
                        lbtndeleteFolder.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvFolder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["FOLDERID"] = id;
                if (e.CommandName == "EditZone")
                {
                    edit_Folder(id);
                    btnupdateFolder.Visible = true;
                    btnSubmitFolder.Visible = false;
                }
                else if (e.CommandName == "DeleteZone")
                {
                    delete_Folder(id);
                    bind_Folder();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSubFolder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["SUBFOLDERID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_SubFolder(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_SubFolder(id);
                    bind_SubFolder();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void bind_Artifact()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_Artifact", ID: ddlSection.SelectedValue);
                gvArtifact.DataSource = ds.Tables[0];
                gvArtifact.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Artifact()
        {
            try
            {
                string Artifact = txtArtifact.Text;
                Artifact = Artifact.TrimStart();
                Artifact = Artifact.TrimEnd();
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "insert_Artifact", ID: ddlSection.SelectedValue, Artifact_Name: Artifact, SEQNO: txtArtifactSeq.Text, RefNo: txtArtifactRef.Text, DEFINITION: txtDefinition.Text);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Artifact Added successfully.')", true);
                bind_Artifact();
                bind_GVSubFolder();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Artifact()
        {
            try
            {
                string Artifact = txtArtifact.Text;
                Artifact = Artifact.TrimStart();
                Artifact = Artifact.TrimEnd();
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "update_Artifact", ID: Session["ArtifactID"].ToString(), Artifact_Name: Artifact, SEQNO: txtArtifactSeq.Text, RefNo: txtArtifactRef.Text, SubFolder_ID: ddlSection.SelectedValue, DEFINITION: txtDefinition.Text);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Artifact updated successfully.')", true);

                bind_Artifact();
                bind_GVSubFolder();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Artifact(string ID)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "select_Artifact", ID: ID);
                txtArtifact.Text = ds.Tables[0].Rows[0]["Artifact_Name"].ToString();
                txtArtifactSeq.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtArtifactRef.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();
                txtDefinition.Text = ds.Tables[0].Rows[0]["DEFINITION"].ToString();

                btnupdateArtifact.Visible = true;
                btnsubmitArtifact.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Artifact(string ID)
        {
            try
            {

                dal_eTMF.eTMF_SET_SP(ACTION: "delete_Artifact", ID: ID);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Artifact deleted successfully.')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Artifact();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitArtifact_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Artifact();
                txtArtifact.Text = "";
                txtArtifactSeq.Text = "";
                txtArtifactRef.Text = "";
                txtDefinition.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateArtifact_Click(object sender, EventArgs e)
        {
            try
            {
                update_Artifact();
                txtArtifact.Text = "";
                txtArtifactSeq.Text = "";
                txtArtifactRef.Text = "";
                txtDefinition.Text = "";
                btnupdateArtifact.Visible = false;
                btnsubmitArtifact.Visible = true;
                bind_Artifact();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelArtifact_Click(object sender, EventArgs e)
        {
            try
            {
                txtArtifact.Text = "";
                txtArtifactSeq.Text = "";
                txtArtifactRef.Text = "";
                txtDefinition.Text = "";
                btnupdateArtifact.Visible = false;
                btnsubmitArtifact.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSubFolder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteFolder = (e.Row.FindControl("lbtndeleteSubFolder") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteFolder.Visible = false;
                    }
                    else
                    {
                        lbtndeleteFolder.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvArtifact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["ArtifactID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Artifact(id);
                    btnupdateArtifact.Visible = true;
                    btnsubmitArtifact.Visible = false;
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Artifact(id);
                    bind_Artifact();
                    bind_GVSubFolder();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvArtifact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    HiddenField ArtifactID = (e.Row.FindControl("ArtifactID") as HiddenField);
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GET_ARTIFACTCOUNT", ID: ArtifactID.Value);
                    string Count = ds.Tables[0].Rows[0]["COUNT"].ToString();
                    LinkButton lbtndeleteFolder = (e.Row.FindControl("lbtndeleteSubFolder") as LinkButton);

                    if (Convert.ToInt32(Count) > 0)
                    {
                        lbtndeleteFolder.Visible = false;
                    }
                    else
                    {
                        lbtndeleteFolder.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbnDocumentTypeExport_Click(object sender, EventArgs e)
        {
            
            try
            {
                string xlname = "Document-Type.xls";

                DataSet ds = dal_eTMF.eTMF_LOG_SP(
                     ACTION: "DOCUMENTTYPE_AUDITTRAIL"
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

        protected void lbnZoneExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Zone.xls";

                DataSet ds = dal_eTMF.eTMF_LOG_SP(
                     ACTION: "ZONE_AUDITTRAIL"
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
         
        protected void lbtnSectionExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Section.xls";

                DataSet ds = dal_eTMF.eTMF_LOG_SP(
                     ACTION: "SECTION_AUDITTRAIL"
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

        protected void lbtnArtifactExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Artifact.xls";

                DataSet ds = dal_eTMF.eTMF_LOG_SP(
                     ACTION: "ARTIFACT_AUDITTRAIL"
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

        protected void gvFolder_PreRender(object sender, EventArgs e)
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