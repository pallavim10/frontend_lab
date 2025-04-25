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
    public partial class eTMF_DOC_Review : System.Web.UI.Page
    {
        DAL dal = new DAL();
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
                        DataSet ds = dal.eTMF_SP(ACTION: "GET_eTMF_DATA_FOR_REVIEW", ID: Request.QueryString["ID"].ToString());

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["DOC_ACTION"].ToString() == "UPLOADTOETMF")
                            {
                                divforeTMF.Visible = true;
                                divfortask.Visible = false;
                                divDefaultView.Visible = true;
                                divdocument.Visible = true;

                                lblStructure.Text = ds.Tables[0].Rows[0]["STRUCTURE"].ToString();

                                lblZone.Text = ds.Tables[0].Rows[0]["ZONE"].ToString();

                                lblSection.Text = ds.Tables[0].Rows[0]["SECTION"].ToString();

                                lblArtifacts.Text = ds.Tables[0].Rows[0]["ARTIFACT"].ToString();

                                lblDocument.Text = ds.Tables[0].Rows[0]["DocName"].ToString();

                                lblAction.Text = ds.Tables[0].Rows[0]["Status"].ToString();

                                lblDeadlineDate.Text = ds.Tables[0].Rows[0]["DeadlineDate"].ToString();

                                lblCountry.Text = ds.Tables[0].Rows[0]["COUNTRY"].ToString();

                                lblSiteId.Text = ds.Tables[0].Rows[0]["SiteID"].ToString();

                                lblExpireDate.Text = ds.Tables[0].Rows[0]["ExpiryDate"].ToString();

                                lblfilename.Text = ds.Tables[0].Rows[0]["UploadFileName"].ToString();
                            }
                            else if (ds.Tables[0].Rows[0]["DOC_ACTION"].ToString() == "UPLOADFORTASK")
                            {
                                divforeTMF.Visible = false;
                                divfortask.Visible = true;
                                divDefaultView.Visible = true;
                                divdocument.Visible = true;

                                lblDepartment.Text = ds.Tables[0].Rows[0]["DEPARTMENT"].ToString();

                                lblTask.Text = ds.Tables[0].Rows[0]["TASK"].ToString();

                                lblSubTask.Text = ds.Tables[0].Rows[0]["SUBTASK"].ToString();

                                lblDocument.Text = ds.Tables[0].Rows[0]["DocName"].ToString();

                                lblAction.Text = ds.Tables[0].Rows[0]["Status"].ToString();

                                lblDeadlineDate.Text = ds.Tables[0].Rows[0]["DeadlineDate"].ToString();

                                lblCountry.Text = ds.Tables[0].Rows[0]["COUNTRY"].ToString();

                                lblSiteId.Text = ds.Tables[0].Rows[0]["SiteID"].ToString();

                                lblExpireDate.Text = ds.Tables[0].Rows[0]["ExpiryDate"].ToString();

                                lblfilename.Text = ds.Tables[0].Rows[0]["UploadFileName"].ToString();
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "Update_Doc_Review", ID: Request.QueryString["ID"].ToString());
                Response.Redirect(Session["prevURL"].ToString(), false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string PAGENAME = Session["prevURL"].ToString();
            Response.Redirect(PAGENAME);
        }
    }
}