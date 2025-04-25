using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class CTMS_UploadDoc : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["FolderID"] != null)
                    {
                        lbl1.Text = "Folder";
                        lbl2.Text = "Sub-Folder";

                        lbl_Task_ID.Text = Request.QueryString["FolderID"].ToString();
                        lbl_Sub_Task_ID.Text = Request.QueryString["SubFolderID"].ToString();

                        DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "select_Folder", ID: lbl_Task_ID.Text);
                        lbl_Task.Text = ds.Tables[0].Rows[0]["Folder"].ToString();

                        DataSet ds1 = dal_eTMF.eTMF_SET_SP(ACTION: "select_SubFolder", ID: lbl_Sub_Task_ID.Text);
                        lbl_Sub_Task.Text = ds1.Tables[0].Rows[0]["Sub_Folder_Name"].ToString();
                    }
                    else
                    {
                        lbl1.Text = "Task";
                        lbl2.Text = "Sub-Task";

                        lbl_Task_ID.Text = Request.QueryString["TaskID"].ToString();
                        lbl_Sub_Task_ID.Text = Request.QueryString["SubTaskID"].ToString();

                        DataSet ds = dal.Budget_SP(Action: "single_Task", Task_ID: lbl_Task_ID.Text);
                        lbl_Task.Text = ds.Tables[0].Rows[0]["Task_Name"].ToString();

                        DataSet ds1 = dal.Budget_SP(Action: "single_SubTask", Task_ID: lbl_Task_ID.Text, Sub_Task_ID: lbl_Sub_Task_ID.Text);
                        lbl_Sub_Task.Text = ds1.Tables[0].Rows[0]["Sub_Task_Name"].ToString();
                    }


                    if (Request.QueryString["INVID"].ToString() == "0")
                    {
                        divINVID.Visible = false;
                    }
                    else if (Request.QueryString["INVID"].ToString() == "9999")
                    {
                        GetSites();
                    }
                    else
                    {
                        drpSites.Items.Insert(0, new ListItem(Request.QueryString["INVID"].ToString(), Request.QueryString["INVID"].ToString()));
                        drpSites.SelectedValue = Request.QueryString["INVID"].ToString();
                    }
                }

                FileUpload1.Attributes["multiple"] = "multiple";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSites()
        {
            try
            {
                DataSet ds = dal.GetSiteID(Action: "INVID", PROJECTID: Session["PROJECTID"].ToString(), User_ID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        drpSites.DataSource = ds.Tables[0];
                        drpSites.DataValueField = "INVNAME";
                        drpSites.DataBind();
                    }
                    else
                    {
                        drpSites.DataSource = ds.Tables[0];
                        drpSites.DataValueField = "INVNAME";
                        drpSites.DataBind();
                        drpSites.Items.Insert(0, new ListItem("None", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Upload()
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile uploadedFile = Request.Files[i];

                    string filename = Path.GetFileName(uploadedFile.FileName);
                    if (filename == "")
                    {
                        throw new Exception("PLEASE SELECT PDF DOCUMENT TO UPLOAD");
                    }
                    string contentType = uploadedFile.ContentType;

                    if (contentType != "application/pdf")
                    {
                        Response.Write("<script> alert('PLEASE SELECT PDF DOCUMENT TO UPLOAD.')</script>");
                    }
                    else
                    {
                        using (Stream fs = uploadedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                if (Request.QueryString["FolderID"] != null)
                                {
                                    if (drpSites.SelectedValue != "0")
                                    {
                                        dal_eTMF.eTMF_SET_SP(
                                        ACTION: "Upload",
                                        Project_ID: Session["PROJECTID"].ToString(),
                                        Folder_ID: lbl_Task_ID.Text,
                                        SubFolder_ID: lbl_Sub_Task_ID.Text,
                                        INVID: drpSites.SelectedValue,
                                        FileName: filename.ToString(),
                                        ContentType: contentType,
                                        Data: Convert.ToInt32(bytes).ToString(),
                                        User: Session["User_ID"].ToString()
                                            );
                                    }
                                    else
                                    {
                                        dal_eTMF.eTMF_SET_SP(
                                        ACTION: "Upload",
                                        Project_ID: Session["PROJECTID"].ToString(),
                                        Folder_ID: lbl_Task_ID.Text,
                                        SubFolder_ID: lbl_Sub_Task_ID.Text,
                                        FileName: filename.ToString(),
                                        ContentType: contentType,
                                        Data: Convert.ToInt32(bytes).ToString(),
                                        User: Session["User_ID"].ToString()
                                            );
                                    }
                                }
                                else
                                {
                                    if (drpSites.SelectedValue != "0")
                                    {
                                        dal_eTMF.eTMF_SET_SP(
                                        ACTION: "Upload",
                                        Project_ID: Session["PROJECTID"].ToString(),
                                        Task_ID: lbl_Task_ID.Text,
                                        Sub_Task_ID: lbl_Sub_Task_ID.Text,
                                        INVID: drpSites.SelectedValue,
                                        FileName: filename.ToString(),
                                        ContentType: contentType,
                                        Data: Convert.ToInt32(bytes).ToString(),
                                        User: Session["User_ID"].ToString()
                                            );
                                    }
                                    else
                                    {
                                        dal_eTMF.eTMF_SET_SP(
                                        ACTION: "Upload",
                                        Project_ID: Session["PROJECTID"].ToString(),
                                        Task_ID: lbl_Task_ID.Text,
                                        Sub_Task_ID: lbl_Sub_Task_ID.Text,
                                        FileName: filename.ToString(),
                                        ContentType: contentType,
                                        Data: Convert.ToInt32(bytes).ToString(),
                                        User: Session["User_ID"].ToString()
                                            );
                                    }
                                }
                            }
                        }

                        Response.Write("<script> alert('Document Uploaded successfully.')</script>");
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Upload();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}