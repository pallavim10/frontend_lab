using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class NewIssuePopup : System.Web.UI.Page
    {
        DAL dal = new DAL();

        string INVID = "";
        string SubjectID = "";
        string Source = "";
        string Department = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SubjectID = Request.QueryString["Subject"];

                if (Request.QueryString["INVID"] != null)
                {
                    INVID = Request.QueryString["INVID"];
                }
                else
                {
                    DataSet ds = dal.GetSiteID(
                    Action: "Get_INVID_BySubjectID",
                    INVID: SubjectID
                    );

                    INVID = ds.Tables[0].Rows[0][0].ToString();
                }

                Department = Request.QueryString["Department"];

                Source = Request.QueryString["Source"];

                if (Request.QueryString["SUB_TEST"] == null)
                {
                    if (Request.QueryString["EventCode"] != null)
                    {
                        txtEventCode.Text = Request.QueryString["EventCode"];
                    }
                    if (Request.QueryString["Rule"] != null)
                    {
                        txtRule.Text = Request.QueryString["Rule"];
                    }
                }
                if (!this.IsPostBack)
                {
                    if (Session["PROJECTID"] != null)
                    {
                        Drp_Project.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                        fill_Inv();
                        if (INVID != null)
                        {
                            drp_InvID.SelectedValue = INVID;
                            fill_Subj();
                            drp_SUBJID.SelectedValue = SubjectID;
                        }
                    }
                    else
                    {
                        fill_Project();
                    }
                    getData();

                    txtSource.Text = Source;
                    drp_DEPT.SelectedValue = Department;

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void fill_Project()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project.DataSource = ds.Tables[0];
                Drp_Project.DataValueField = "Project_ID";
                Drp_Project.DataTextField = "PROJNAME";
                Drp_Project.DataBind();
                Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void fill_Inv()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSiteID(
                Action: "INVID",
                PROJECTID: Session["PROJECTID"].ToString(),
                User_ID: Session["User_ID"].ToString()
                );
                drp_InvID.DataSource = ds.Tables[0];
                drp_InvID.DataValueField = "INVNAME";
                drp_InvID.DataBind();
                drp_InvID.Items.Insert(0, new ListItem("--Select Inv ID--", "0"));

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Subj()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSiteID(
                Action: "SUBJECT",
                PROJECTID: Session["PROJECTID"].ToString(),
                INVID: drp_InvID.SelectedValue,
                User_ID: Session["User_ID"].ToString()
                );
                drp_SUBJID.DataSource = ds.Tables[0];
                drp_SUBJID.DataValueField = "SUBJID";
                drp_SUBJID.DataBind();
                drp_SUBJID.Items.Insert(0, new ListItem("--Select SUBJ ID--", "0"));
                if (SubjectID != "")
                {
                    drp_SUBJID.SelectedValue = SubjectID;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_PDCode1()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.getsetPDCode(
                Action: "Get_Category",
                NATURE: drp_Nature.SelectedValue
                );
                drp_PDCode1.DataSource = ds.Tables[0];
                drp_PDCode1.DataValueField = "CategoryID";
                drp_PDCode1.DataTextField = "Category";
                drp_PDCode1.DataBind();
                drp_PDCode1.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_PDCode2()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.getsetPDCode(
                Action: "Get_SubCategory",
                NATURE: drp_Nature.SelectedValue,
                PDCODE1: drp_PDCode1.SelectedValue
                );
                drp_PDCode2.DataSource = ds.Tables[0];
                drp_PDCode2.DataValueField = "SubCategoryID";
                drp_PDCode2.DataTextField = "SubCategory";
                drp_PDCode2.DataBind();
                drp_PDCode2.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void getData()
        {
            try
            {

                DAL dal;
                dal = new DAL();

                DataSet ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "DEPT");
                dal.BindDropDown(drp_DEPT, ds.Tables[0]);

                //ds = new DataSet();
                //ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
                //dal.BindDropDown(drp_Status, ds.Tables[0]);

                drp_Status.Items.Clear();
                drp_Status.Items.Insert(0, new ListItem("New", "New"));

                ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Priority");
                dal.BindDropDown(drp_Priority, ds.Tables[0]);

                dal = new DAL();
                ds = dal.getsetPDCode(
                Action: "Get_Nature",
                NATURE: drp_Nature.SelectedValue,
                PDCODE1: drp_PDCode1.SelectedValue
                );
                drp_Nature.DataSource = ds.Tables[0];
                drp_Nature.DataValueField = "NatureID";
                drp_Nature.DataTextField = "Nature";
                drp_Nature.DataBind();
                drp_Nature.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (drp_Nature.SelectedValue == "Protocol Deviation" || drp_Nature.SelectedValue == "Process Deviation")
                {
                    if (drp_PDCode1.SelectedValue == "0")
                    {
                        throw new System.ArgumentException("Category is Mandatory");
                    }
                }


                if (drp_Nature.SelectedValue == "Protocol Deviation" || drp_Nature.SelectedValue == "Process Deviation")
                {
                    if (drp_PDCode2.SelectedValue == "0")
                    {
                        throw new System.ArgumentException("Sub Category is Mandatory");
                    }
                }

                DAL dal;
                dal = new DAL();

                if (Request.QueryString["PVID"] != null)
                {
                    DataSet ds = new DataSet();
                    ds = dal.getsetISSUES(Action: "INSERT",
                    Project_ID: Drp_Project.SelectedValue,
                    INVID: drp_InvID.SelectedValue,
                    SUBJID: drp_SUBJID.SelectedValue,
                    Department: drp_DEPT.SelectedValue,
                    Summary: txtSummary.Text,
                    Status: drp_Status.SelectedValue,
                    Priority: drp_Priority.SelectedValue,
                    Description: txtDescription.Text,
                    ISSUEBy: Session["User_ID"].ToString(),
                    Source: txtSource.Text,
                    Refrence: txtReference.Text,
                    Nature: drp_Nature.SelectedValue,
                    PDCODE1: drp_PDCode1.SelectedItem.Value,
                    PD1Catagory: drp_PDCode1.SelectedItem.Text,
                    PDCODE2: drp_PDCode2.SelectedItem.Value,
                    PD2Catagory: drp_PDCode2.SelectedItem.Text,
                    EventCode: txtEventCode.Text,
                    Rule: txtRule.Text,
                    ENTEREDBY: Session["User_ID"].ToString(),
                    PVID: Request.QueryString["PVID"].ToString(),
                    RECID: Request.QueryString["RECID"].ToString()
                    );
                }
                else
                {
                    DataSet ds = new DataSet();
                    ds = dal.getsetISSUES(Action: "INSERT",
                    Project_ID: Drp_Project.SelectedValue,
                    INVID: drp_InvID.SelectedValue,
                    SUBJID: drp_SUBJID.SelectedValue,
                    Department: drp_DEPT.SelectedValue,
                    Summary: txtSummary.Text,
                    Status: drp_Status.SelectedValue,
                    Priority: drp_Priority.SelectedValue,
                    Description: txtDescription.Text,
                    ISSUEBy: Session["User_ID"].ToString(),
                    Source: txtSource.Text,
                    Refrence: txtReference.Text,
                    Nature: drp_Nature.SelectedValue,
                    PDCODE1: drp_PDCode1.SelectedItem.Value,
                    PD1Catagory: drp_PDCode1.SelectedItem.Text,
                    PDCODE2: drp_PDCode2.SelectedItem.Value,
                    PD2Catagory: drp_PDCode2.SelectedItem.Text,
                    EventCode: txtEventCode.Text,
                    Rule: txtRule.Text,
                    ENTEREDBY: Session["User_ID"].ToString()
                    );
                }

                DataSet ds2 = dal.getsetAttachments(Action: "GET_ISSUES_ID");
                string ID = ds2.Tables[0].Rows[0]["ISSUES_ID"].ToString();
                Upload_Single(ID);


                //----------------------------------------Email Section---------------------------------------//
                CommonFunction.CommonFunction CF = new CommonFunction.CommonFunction();
                DataSet dsEmailDetails;
                dsEmailDetails = dal.Get_Email_Details
                (
                ProjectID: Drp_Project.SelectedValue,
                INVID: drp_InvID.SelectedValue,
                Event_ID: "52"
                );
                if (dsEmailDetails.Tables[0].Rows.Count > 0)
                {
                    DataRow drEmailDetails = dsEmailDetails.Tables[0].Rows[0];
                    string E_Address, E_CcAdd, E_Subject, E_Body;
                    E_Body = "Issue is  Raised For Site Id: " + drp_InvID.SelectedValue + ", Subject Id: " + SubjectID + ", Event Code: " + txtEventCode.Text + ", Rule: " + txtRule.Text + ", Issue Detail- " + txtSummary.Text + ", User: " + Session["User_Name"].ToString() + ".";
                    E_Address = drEmailDetails["E_TO"].ToString();
                    E_CcAdd = drEmailDetails["E_CC"].ToString();
                    E_Subject = "Protocol ID-" + Session["PROJECTIDTEXT"].ToString() + ":Issue Raised. Site Id: " + drp_InvID.SelectedValue + ", Subject Id: " + SubjectID + ", Rule:" + txtRule.Text + ".";
                    CF.Email_Users(E_Address, E_CcAdd, E_Subject, E_Body);
                }
                //----------------------------------------Email Section---------------------------------------//

                Response.Write("<script> alert('Record Updated successfully.'); </script>");
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drp_Nature_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                fill_PDCode1();
                fill_PDCode2();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }


        }

        protected void drp_PDCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                fill_PDCode2();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drp_InvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Subj();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //protected void Upload(string ID)
        //{
        //    try
        //    {
        //        if (grdAttachment.Rows.Count > 0)
        //        {
        //            int rowindex;
        //            for (rowindex = 0; rowindex < grdAttachment.Rows.Count; rowindex++)
        //            {
        //                DAL dal;
        //                dal = new DAL();
        //                DataSet ds = new DataSet();
        //                ds = dal.getsetAttachments(
        //                Action: "INSERT",
        //                ISSUES_ID: ID,
        //                Name:((TextBox)grdAttachment.Rows[rowindex].FindControl("Name")).ToString(),
        //                ContentType: ((TextBox)grdAttachment.Rows[rowindex].FindControl("ContentType")).ToString(),
        //                //Attachments: (byte[])(grdAttachment.Rows[rowindex].FindControl("Attachments"))
        //                );
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = "";
        //        lblErrorMsg.Text = ex.Message;
        //    }
        //}

        protected void upload_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (filename == "")
                {
                    //throw new Exception("PLEASE SELECT DOCUMENT TO UPLOAD");
                }
                string contentType = FileUpload1.PostedFile.ContentType;

                using (Stream fs = FileUpload1.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        DAL dal;
                        dal = new DAL();
                        DataSet ds = new DataSet();
                        ds = dal.getsetAttachments(
                        Action: "INSERT_TEMP",
                        Name: filename,
                        ContentType: contentType,
                        Attachments: bytes
                        );

                        //Load_Attachment();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }

        }

        //private void Load_Attachment()
        //{

        //    try
        //    {

        //        DAL dal;
        //        dal = new DAL();

        //        DataSet ds = new DataSet();
        //        ds = dal.getsetAttachments(Action: "GET_TEMP");
        //        grdAttachment.DataSource = ds.Tables[0];
        //        grdAttachment.DataBind();

        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }

        //}

        protected void Upload_Single(string ID)
        {
            try
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (filename == "")
                {
                    // throw new Exception("PLEASE SELECT DOCUMENT TO UPLOAD");
                }
                string contentType = FileUpload1.PostedFile.ContentType;

                using (Stream fs = FileUpload1.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        DAL dal;
                        dal = new DAL();
                        DataSet ds = new DataSet();
                        ds = dal.getsetAttachments(
                        Action: "INSERT",
                        ISSUES_ID: ID,
                        Name: filename,
                        ContentType: contentType,
                        Attachments: bytes
                        );

                        //Load_Attachment();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }

        }
    }
}