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

namespace CTMS
{
    public partial class Training_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                FileUpload1.Attributes["multiple"] = "multiple";

                if (!IsPostBack)
                {
                    get_Section();
                    btnupdateSec.Visible = false;
                    btnupdateSubSec.Visible = false;
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
                gvSec.DataSource = ds.Tables[0];
                gvSec.DataBind();

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

        private void get_SectionGV()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_Section");
                gvSec.DataSource = ds.Tables[0];
                gvSec.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Section(string id)
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "select_Section", ID: id);
                txtSec.Text = ds.Tables[0].Rows[0]["Section"].ToString();
                btnsubmitSec.Visible = false;
                btnupdateSec.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Section()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "insert_Section", Section: txtSec.Text);
                string ID = ds.Tables[0].Rows[0]["ID"].ToString();
                upload_Attachments(ID);
                txtSec.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void upload_Attachments(string ID)
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile uploadedFile = Request.Files[i];

                    string filename = Path.GetFileName(uploadedFile.FileName);
                    if (filename != "")
                    {
                        string contentType = uploadedFile.ContentType;

                        using (Stream fs = uploadedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                dal.Training_SP(Action: "Upload", Section_ID: ID, FileName: filename, ContentType: contentType, Data: bytes);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Section()
        {
            try
            {
                dal.Training_SP(Action: "update_Section", Section: txtSec.Text, ID: Session["SECID"].ToString());
                upload_Attachments(Session["SECID"].ToString());
                txtSec.Text = "";
                btnsubmitSec.Visible = true;
                btnupdateSec.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Section(string ID)
        {
            try
            {
                dal.Training_SP(Action: "delete_Section", ID: ID);
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
                DataSet ds = dal.Training_SP(Action: "get_SubSection", Section_ID: ddlSec.SelectedValue);
                gvSubSec.DataSource = ds.Tables[0];
                gvSubSec.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_SubSection(string id)
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "select_SubSection", ID: id);
                txtSubSec.Text = ds.Tables[0].Rows[0]["SubSection"].ToString();
                ddlSec.Enabled = false;
                btnsubmitSubSec.Visible = false;
                btnupdateSubSec.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_SubSec()
        {
            try
            {
                dal.Training_SP(Action: "insert_SubSection", SubSection: txtSubSec.Text, Section_ID: ddlSec.SelectedValue);
                txtSubSec.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_SubSec()
        {
            try
            {
                dal.Training_SP(Action: "update_SubSection", SubSection: txtSubSec.Text, ID: Session["SUBSECID"].ToString());
                txtSubSec.Text = "";
                btnupdateSubSec.Visible = false;
                btnsubmitSubSec.Visible = true;
                ddlSec.Enabled = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_SubSec(string id)
        {
            try
            {
                dal.Training_SP(Action: "delete_SubSection", ID: id);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitSec_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Section();
                get_Section();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateSec_Click(object sender, EventArgs e)
        {
            try
            {
                update_Section();
                get_Section();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelSec_Click(object sender, EventArgs e)
        {
            try
            {
                txtSec.Text = "";
                btnsubmitSec.Visible = true;
                btnupdateSec.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSec_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["SECID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Section(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Section(id);
                    get_Section();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteSec = (e.Row.FindControl("lbtndeleteSec") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteSec.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSec.Visible = true;
                    }
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

        protected void btnsubmitSubSec_Click(object sender, EventArgs e)
        {
            try
            {
                insert_SubSec();
                get_SectionGV();
                get_SubSection();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateSubSec_Click(object sender, EventArgs e)
        {
            try
            {
                update_SubSec();
                get_SubSection();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelSubSec_Click(object sender, EventArgs e)
        {
            try
            {
                txtSubSec.Text = "";
                btnupdateSubSec.Visible = false;
                btnsubmitSubSec.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSubSec_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["SUBSECID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_SubSection(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_SubSec(id);
                    get_SectionGV();
                    get_SubSection();
                }
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
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteSubSec = (e.Row.FindControl("lbtndeleteSubSec") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteSubSec.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSubSec.Visible = true;
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