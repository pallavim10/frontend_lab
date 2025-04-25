using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Xml.Linq;

namespace CTMS
{
    public partial class IWRS_USER_MANUAL : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (!IsPostBack)
                {
                    UserManual();
                }
            }
        }

        private void UserManual()
        {
            try
            {
                DAL_IWRS dal_IWRS = new DAL_IWRS();
                DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_USER_MANUAL", QUECODE: "USERMANUAL");

                string FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                string ContentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                byte[] FileData = (byte[])ds.Tables[0].Rows[0]["DATA"];

                if (!Directory.Exists(Server.MapPath("~/Manuals/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Manuals/"));
                }

                string FilePath = Server.MapPath("~/Manuals/") + FileName;

                using (System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
                {
                    using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
                    {
                        bw.Write(FileData);
                        bw.Close();
                    }
                }

                string newPdfUrl = ("Manuals/") +FileName;
                ScriptManager.RegisterStartupScript(this, GetType(), "ChangePDFSource", "changePDFSource('" + newPdfUrl + "');", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}