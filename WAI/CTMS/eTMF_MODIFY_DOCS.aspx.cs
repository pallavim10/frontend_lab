using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_MODIFY_DOCS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        CommonFunction.CommonFunction com = new CommonFunction.CommonFunction();
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
                    if (!this.IsPostBack)
                    {
                        divstatus.Visible = true;
                        divDefaultView.Visible = true;
                        divCountry.Visible = true;
                        divINVID.Visible = true;
                        divIndividual.Visible = true;
                        divforeTMF.Visible = true;
                        divdocument.Visible = true;

                        BIND_DOCUMENTTYPE();
                        GetCountry();
                        GetSite();
                        GetUsers();
                        CHECK_VERSION_TYPE();
                        GET_INDIVIDUAL();
                        GETDATA();

                        Response.Write("<script> ShowReminderDiv();</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "GET_eTMF_DOC_DATA", ID: Request.QueryString["ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    BIND_DOCUMENTTYPE();
                    drpDocType.SelectedValue = ds.Tables[0].Rows[0]["DocTypeID"].ToString();

                    BIND_Zone();
                    ddlZone.SelectedValue = ds.Tables[0].Rows[0]["ZoneId"].ToString();

                    BIND_Sections();
                    ddlSections.SelectedValue = ds.Tables[0].Rows[0]["SectionId"].ToString();

                    BIND_Artifacts();
                    ddlArtifacts.SelectedValue = ds.Tables[0].Rows[0]["ArtifactId"].ToString();

                    BIND_DOCUMENTS();
                    drpDocument.SelectedValue = ds.Tables[0].Rows[0]["DocID"].ToString();

                    drpDocument_SelectedIndexChanged(this, EventArgs.Empty);

                    BIND_SPEC();
                    txtSPEC.Text = ds.Tables[0].Rows[0]["SPEC"].ToString();

                    drpAction.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();

                    drpAction_SelectedIndexChanged(this, EventArgs.Empty);

                    txtDeadline.Text = ds.Tables[0].Rows[0]["DeadlineDate"].ToString();

                    txtDovVersionNo.Text = ds.Tables[0].Rows[0]["DocVERSION"].ToString();
                    txtDocDateTime.Text = ds.Tables[0].Rows[0]["DocDATE"].ToString();
                    txtNote.Text = ds.Tables[0].Rows[0]["NOTE"].ToString();
                    txtReceiptdate.Text = ds.Tables[0].Rows[0]["RECEIPTDAT"].ToString();

                    ddlStatus3.SelectedValue = ds.Tables[0].Rows[0]["ACTION"].ToString();
                    ddlIndividual.SelectedValue = ds.Tables[0].Rows[0]["INDIVIDUAL"].ToString();

                    GetCountry();
                    drpCountry.SelectedValue = ds.Tables[0].Rows[0]["CountryID"].ToString();

                    GetSite();
                    drpSites.SelectedValue = ds.Tables[0].Rows[0]["SiteID"].ToString();

                    txtExpiryDate.Text = ds.Tables[0].Rows[0]["ExpiryDate"].ToString();

                    txtFileName1.Text = ds.Tables[0].Rows[0]["UploadFileName"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_Zone();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_Sections();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_Artifacts();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlArtifacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_DOCUMENTS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_DOCUMENTTYPE()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_DocType");

                drpDocType.DataSource = ds.Tables[0];
                drpDocType.DataValueField = "ID";
                drpDocType.DataTextField = "DocType";
                drpDocType.DataBind();
                drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_Zone()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Folder", DOCTYPEID: drpDocType.SelectedValue);

                ddlZone.DataSource = ds.Tables[0];
                ddlZone.DataValueField = "ID";
                ddlZone.DataTextField = "Folder";
                ddlZone.DataBind();
                ddlZone.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_Sections()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Sub_Folder", FOLDERID: ddlZone.SelectedValue);

                ddlSections.DataSource = ds.Tables[0];
                ddlSections.DataValueField = "ID";
                ddlSections.DataTextField = "Sub_Folder_Name";
                ddlSections.DataBind();
                ddlSections.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_Artifacts()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_ARTIFACTS", SUBFOLDERID: ddlSections.SelectedValue);

                ddlArtifacts.DataSource = ds.Tables[0];
                ddlArtifacts.DataValueField = "ID";
                ddlArtifacts.DataTextField = "Artifact_Name";
                ddlArtifacts.DataBind();
                ddlArtifacts.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BIND_SPEC()
        {
            try
            {
                txtSPEC.Text = "";
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_eTMF_SPEC", DocID: drpDocument.SelectedValue);
                string Values = "";
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Values += "" + ds.Tables[0].Rows[i]["TEXT"].ToString() + ",";
                    }
                }
                hfSPECS.Value = Values.TrimEnd(',');

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindSpecs();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BIND_DOCUMENTS()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GetDocument_List_eTMF",
                     UploadZoneId: ddlZone.SelectedValue,
                     UploadSectionId: ddlSections.SelectedValue,
                     UploadArtifactId: ddlArtifacts.SelectedValue
                     );

                drpDocument.DataSource = ds;
                drpDocument.DataValueField = "ID";
                drpDocument.DataTextField = "DocName";
                drpDocument.DataBind();
                drpDocument.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetCountry()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP(USERID: Session["User_ID"].ToString());
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataTextField = "COUNTRYNAME";
                drpCountry.DataValueField = "COUNTRYID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSite()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(COUNTRYID: drpCountry.SelectedValue, USERID: Session["User_ID"].ToString());
                drpSites.DataSource = ds.Tables[0];
                drpSites.DataTextField = "INVID";
                drpSites.DataValueField = "INVID";
                drpSites.DataBind();
                drpSites.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetSite();
                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_INDIVIDUAL();
                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE();
                // CLAER();
                Response.Write("<script> alert('Document Updated successfully.');window.location='ETMF_MODI_DEL_DOCS.aspx';</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void UPDATE()
        {
            try
            {
                string DocTypeId = "", ZoneId = "", SectionId = "", ArtifactId = "", FUNCTIONS = "";

                if (drpDocType.SelectedValue == "")
                {
                    DocTypeId = "0";
                }
                else
                {
                    DocTypeId = drpDocType.SelectedValue;
                }

                if (ddlZone.SelectedValue == "")
                {
                    ZoneId = "0";
                }
                else
                {
                    ZoneId = ddlZone.SelectedValue;
                }

                if (ddlSections.SelectedValue == "")
                {
                    SectionId = "0";
                }
                else
                {
                    SectionId = ddlSections.SelectedValue;
                }

                if (ddlArtifacts.SelectedValue == "")
                {
                    ArtifactId = "0";
                }
                else
                {
                    ArtifactId = ddlArtifacts.SelectedValue;
                }

                FUNCTIONS = "0";

                UPDATE_DOCUMENT(txtFileName1.Text, DocTypeId, ZoneId, SectionId, ArtifactId);

                //CLAER();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void UPDATE_DOCUMENT(string UploadFileName, string DocTypeId, string ZoneId, string SectionId, string ArtifactId)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP
                        (
                            ACTION: "UPDATE_DOCUMENT",
                            CountryID: drpCountry.SelectedValue,
                            SiteID: drpSites.SelectedValue,
                            DocID: drpDocument.SelectedValue,
                            DocName: drpDocument.SelectedItem.Text,
                            UploadFileName: UploadFileName,
                            Status: drpAction.SelectedValue,
                            ExpiryDate: txtExpiryDate.Text,
                            DeadlineDate: txtDeadline.Text,
                            UploadDocTypeid: DocTypeId,
                            UploadZoneId: ZoneId,
                            UploadSectionId: SectionId,
                            UploadArtifactId: ArtifactId,
                            DOC_VERSIONNO: txtDovVersionNo.Text,
                            DOC_DATETIME: txtDocDateTime.Text,
                            NOTE: txtNote.Text,
                            RECEIPTDAT :txtReceiptdate.Text,
                            INDIVIDUAL: ddlIndividual.SelectedValue,
                            SUBJID: txtSubject.Text,
                            SPEC: txtSPEC.Text,
                            UPDATEREASON: txtReason.Text,
                            ID: Request.QueryString["ID"].ToString()
                        );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
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

        protected void drpAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
                {
                    divDeadline.Visible = true;
                    txtDeadline.Text = "";
                }
                else
                {
                    divDeadline.Visible = false;
                    txtDeadline.Text = "";
                }

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
                {
                    divUsers.Visible = true;
                }
                else
                {
                    divUsers.Visible = false;
                }

                CHECK_DIVS();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CHECK_DIVS()
        {
            try
            {
                if (drpAction.SelectedValue == "Draft" && hfVerDATE.Value == "False" && hfVerTYPE.Value == "0")
                {
                    divStatus2.Visible = true;
                    divStatus3.Visible = true;
                    ddlFinalStatus.CssClass += " required";
                    ddlStatus3.CssClass += " required";

                    if (ddlStatus3.SelectedValue == "Replace")
                    {
                        divFileReplace.Visible = true;
                    }
                    else
                    {
                        divFileReplace.Visible = false;
                    }
                }
                else if (drpAction.SelectedValue == "Final" && hfVerDATE.Value == "False" && hfVerTYPE.Value == "0")
                {
                    divStatus2.Visible = false;
                    divStatus3.Visible = true;
                    ddlFinalStatus.CssClass = ddlFinalStatus.CssClass.Replace("required", "");
                    ddlStatus3.CssClass += " required";

                    if (ddlStatus3.SelectedValue == "Replace")
                    {
                        divFileReplace.Visible = true;
                    }
                    else
                    {
                        divFileReplace.Visible = false;
                    }
                }
                else
                {
                    divStatus2.Visible = false;
                    divStatus3.Visible = false;
                    ddlFinalStatus.CssClass = ddlFinalStatus.CssClass.Replace("required", "");
                    ddlStatus3.CssClass = ddlFinalStatus.CssClass.Replace("required", "");

                    divFileReplace.Visible = false;
                }

                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_FILES_REPLACE",
                DocID: drpDocument.SelectedValue,
                Status: drpAction.SelectedValue,
                SubStatus: ddlFinalStatus.SelectedValue,
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue,
                INDIVIDUAL: ddlIndividual.SelectedValue
                );

                gvFiles.DataSource = ds;
                gvFiles.DataBind();

                if (gvFiles.Rows.Count == 0)
                {
                    for (int i = 0; i < ddlStatus3.Items.Count; i++)
                    {
                        if (ddlStatus3.Items[i].Value == "Replace")
                        {
                            ddlStatus3.Items[i].Attributes.Add("class", "disp-none");
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < ddlStatus3.Items.Count; i++)
                    {
                        if (ddlStatus3.Items[i].Value == "Replace")
                        {
                            ddlStatus3.Items[i].Attributes.Remove("class");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetUsers()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(
                ACTION: "GetUsers",
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );
                grd_Users.DataSource = ds;
                grd_Users.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUploadAgainDoc_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    UPDATE();

                    Response.Write("<script> alert('Document Updated successfully.');window.location='ETMF_MODI_DEL_DOCS.aspx';</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script> alert('Please select file.')</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void drpDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CHECK_VERSION_TYPE();
                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void CHECK_VERSION_TYPE()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_VERSION_TYPE", DocID: drpDocument.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hfVerDATE.Value = ds.Tables[0].Rows[0]["VerDATE"].ToString();
                    hfVerSPEC.Value = ds.Tables[0].Rows[0]["VerSPEC"].ToString();
                    hfVerTYPE.Value = ds.Tables[0].Rows[0]["VerTYPE"].ToString();

                    lblSPECtitle.Text = ds.Tables[0].Rows[0]["SPECtitle"].ToString();

                    if (ds.Tables[0].Rows[0]["Datetitle"].ToString() != "")
                    {
                        lblDateTitle.Text = ds.Tables[0].Rows[0]["Datetitle"].ToString();
                    }
                    else
                    {
                        lblDateTitle.Text = "Document Date";
                    }

                    if (ds.Tables[0].Rows[0]["Info"].ToString() != "")
                    {
                        lblInstruction.Text = ds.Tables[0].Rows[0]["Info"].ToString();
                        divInstruction.Visible = true;
                    }
                    else
                    {
                        divInstruction.Visible = false;
                    }
                }
                else
                {
                    lblSPECtitle.Text = "";

                    divInstruction.Visible = false;

                    hfVerDATE.Value = "";
                    hfVerSPEC.Value = "";
                    hfVerTYPE.Value = "";
                    hfOwnerEmailId.Value = "";
                }

                if (hfVerDATE.Value == "True")
                {
                    lblRequiredDocDate.Visible = true;
                    txtDocDateTime.CssClass += " required";
                }
                else
                {
                    lblRequiredDocDate.Visible = false;
                    txtDocDateTime.CssClass = txtDocDateTime.CssClass.Replace("required", "");
                }

                BIND_SPEC();
                if (hfVerSPEC.Value == "True")
                {
                    divSPEC.Visible = true;
                    txtSPEC.CssClass += " required";
                }
                else
                {
                    divSPEC.Visible = false;
                    txtSPEC.CssClass = txtSPEC.CssClass.Replace("required", "");
                }

                divCountry.Visible = true;
                divINVID.Visible = true;
                divIndividual.Visible = true;

                drpCountry.CssClass = drpCountry.CssClass.Replace("required", "");
                drpSites.CssClass = drpSites.CssClass.Replace("required", "");
                ddlIndividual.CssClass = ddlIndividual.CssClass.Replace("required", "");
                txtSubject.CssClass = txtSubject.CssClass.Replace("required", "");

                drpCountry.SelectedIndex = 0;
                drpSites.SelectedIndex = 0;
                ddlIndividual.SelectedIndex = 0;
                txtSubject.Text = "";

                if (hfVerTYPE.Value == "Study")
                {
                    divCountry.Visible = false;
                    divINVID.Visible = false;
                    divIndividual.Visible = false;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Country")
                {
                    drpCountry.CssClass += " required";
                    divINVID.Visible = false;
                    divIndividual.Visible = false;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Site")
                {
                    drpCountry.CssClass += " required";
                    drpSites.CssClass += " required";
                    divIndividual.Visible = false;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Individual")
                {
                    ddlIndividual.CssClass += " required";
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Subject")
                {
                    divIndividual.Visible = false;
                    divSubject.Visible = true;
                    txtSubject.CssClass += " required";
                }

                drpAction.SelectedIndex = 0;
                ddlFinalStatus.SelectedIndex = 0;
                ddlStatus3.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void GET_INDIVIDUAL()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(
                ACTION: "GET_INDIVIDUAL",
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );
                ddlIndividual.DataSource = ds.Tables[0];
                ddlIndividual.DataTextField = "User_Name";
                ddlIndividual.DataValueField = "User_ID";
                ddlIndividual.DataBind();
                ddlIndividual.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlFinalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlStatus3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("ETMF_MODI_DEL_DOCS.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}