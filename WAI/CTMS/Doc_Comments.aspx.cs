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
    public partial class Doc_Comments : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    Get_Comments();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Get_Comments()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "Get_Comments", ProjectID: Session["PROJECTID"].ToString(), DocID: Request.QueryString["DocID"].ToString(), SecID: Request.QueryString["SecID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvComments.DataSource = ds;
                    gvComments.DataBind();
                }
                else
                {
                    gvComments.DataSource = null;
                    gvComments.DataBind();
                    lblHeader.Text = "";
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    lblHeader.Text = "Document: " + ds.Tables[1].Rows[0]["DocName"].ToString() + " || " + "Section: " + ds.Tables[1].Rows[0]["SectionName"].ToString();
                }
                else
                {
                    lblHeader.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void INSERT_Comments()
        {
            try
            {
                dal.Documents_SP(Action: "INSERT_Comments", ProjectID: Session["PROJECTID"].ToString(), DocID: Request.QueryString["DocID"].ToString(), SecID: Request.QueryString["SecID"].ToString(), UserID: Session["User_ID"].ToString(), Data: txtComments.Text);
                txtComments.Text = "";
                Get_Comments();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_Comments();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}