using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.IO;

namespace CTMS
{
    public partial class ISSUES : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["PROJECTID"] != null)
                    {
                        Drp_Project.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                        fill_Inv();
                    }
                    else
                    {
                        fill_Project();
                    }
                    getData();
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


        private void fill_Subj()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSiteID(
                Action: "SUBJECT",
                Project_Name: Drp_Project.SelectedItem.Text,
                INVID:drp_InvID.SelectedValue,
                User_ID: Session["User_ID"].ToString()
                );
                drp_SUBJID.DataSource = ds.Tables[0];
                drp_SUBJID.DataValueField = "SUBJID";
                drp_SUBJID.DataBind();
                drp_SUBJID.Items.Insert(0, new ListItem("--Select SUBJ ID--", "0"));
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

                //ds = new DataSet();
                //ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Nature");
                //dal.BindDropDown(drp_Nature, ds.Tables[0]);
                BindCategory();

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
                Source:txtSource.Text,
                Refrence:txtReference.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                Nature:drp_Nature.SelectedItem.Text,
                PDCODE1:drp_PDCode1.SelectedItem.Value,
                PD1Catagory:drp_PDCode1.SelectedItem.Text,
                PDCODE2:drp_PDCode2.SelectedItem.Value,
                PD2Catagory:drp_PDCode2.SelectedItem.Text,
                FactorID:Convert.ToInt32( ddlFactor.SelectedValue),
                Factor:ddlFactor.SelectedItem.Text
               
                );
                DataSet ds2 = dal.getsetAttachments(Action: "GET_ISSUES_ID");
                string ID = ds2.Tables[0].Rows[0]["ISSUES_ID"].ToString();
                Upload_Single(ID);
                Response.Write("<script> alert('Record Updated successfully.');window.location='NewIssues.aspx'; </script>");
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
        
        protected void drp_PDCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSubcategory();
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
                        ISSUES_ID:ID,
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

        protected void drp_PDCode2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindFactors();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        public void BindCategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Category");
                if (dt.Rows.Count > 1)
                {
                    drp_PDCode1.DataSource = dt;
                    drp_PDCode1.DataTextField = "Description";
                    drp_PDCode1.DataValueField = "id";
                    drp_PDCode1.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindSubcategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "SubCategory", Categoryvalue: drp_PDCode1.SelectedValue);
                if (dt.Rows.Count > 1)
                {
                    drp_PDCode2.DataSource = dt;
                    drp_PDCode2.DataTextField = "Description";
                    drp_PDCode2.DataValueField = "id";
                    drp_PDCode2.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindFactors()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Factor", SubCategoryvalue: drp_PDCode2.SelectedValue);
                if (dt.Rows.Count > 1)
                {
                    ddlFactor.DataSource = dt;
                    ddlFactor.DataTextField = "Description";
                    ddlFactor.DataValueField = "id";
                    ddlFactor.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}