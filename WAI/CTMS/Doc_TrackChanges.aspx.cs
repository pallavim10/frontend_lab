using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Text;

namespace CTMS
{
    public partial class Doc_TrackChanges : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetData()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_AuditTrail", ProjectID: Session["PROJECTID"].ToString(), SecID: Request.QueryString["SecID"].ToString(), DocID: Request.QueryString["DocID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvAuditTrail.DataSource = ds;
                    gvAuditTrail.DataBind();
                    lblHeader.Text = "Document: " + ds.Tables[1].Rows[0]["DocName"].ToString() + " || " + "Section: " + ds.Tables[1].Rows[0]["SectionName"].ToString();
                }
                else
                {
                    gvAuditTrail.DataSource = null;
                    gvAuditTrail.DataBind();
                    lblHeader.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}