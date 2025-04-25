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
    public partial class NIWRS_SETUP_DATE : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["IWRS_CurrentDate"] == null)
                {
                    Session["IWRS_CurrentDate"] = cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy");
                }

                if (!Page.IsPostBack)
                {
                    txtCurrentDate.Text = Session["IWRS_CurrentDate"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtCurrentDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session["IWRS_CurrentDate"] = txtCurrentDate.Text;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}