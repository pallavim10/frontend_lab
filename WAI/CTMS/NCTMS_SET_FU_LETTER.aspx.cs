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
    public partial class NCTMS_SET_FU_LETTER : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblMVID.Text = Request.QueryString["SVID"].ToString();
                    GetData();
                }
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
                DataSet ds = dal.CTMS_REPORTS(
                ACTION: "GET_FOLLOWUP_LETTER_DATA",
                SVID: Request.QueryString["SVID"].ToString(),
                USER: Session["User_Id"].ToString()
                );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtFULETTER.Text = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    txtFULETTER.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dal.CTMS_REPORTS(
                ACTION: "SET_FOLLOWUP_LETTER",
                SVID: Request.QueryString["SVID"].ToString(),
                FOLLOWUP: txtFULETTER.Text,
                USER: Session["User_Id"].ToString()
                );

                Response.Redirect("NCTMS_MonitoringVisitList_FU.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}