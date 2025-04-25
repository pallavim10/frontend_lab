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
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace CTMS
{
    public partial class Comm_Inbox : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction comfunc = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

    }
}