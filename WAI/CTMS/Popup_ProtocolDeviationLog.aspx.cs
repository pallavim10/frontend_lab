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
    public partial class Popup_ProtocolDeviationLog : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds;
                ds = new DataSet();

                //Get MH Data
                ds = new DataSet();
                ds = dal.ProtocolVoilation_SP(Action: "GET_DATA_LIST", Project_ID: Session["PROJECTID"].ToString());
                if (ds != null)
                {
                    grdProtVoil.DataSource = ds.Tables[0];
                    grdProtVoil.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}