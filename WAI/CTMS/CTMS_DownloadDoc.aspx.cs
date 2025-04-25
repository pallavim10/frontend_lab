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
    public partial class CTMS_DownloadDoc : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ID = Request.QueryString["ID"].ToString();

                    DownloadFile(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DownloadFile(string ID)
        {
            try
            {
                byte[] bytes;
                string fileName, contentType;

                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "Download", ID: ID);

                bytes = (byte[])ds.Tables[0].Rows[0]["Data"];
                contentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                fileName = ds.Tables[0].Rows[0]["FileName"].ToString();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "Inline; filename=" + fileName);

                // Append cookie
                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);
                // end

                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

                //iFrame.Attributes["src"] = "CTMS_Doc_Handler.ashx?ID=" + ID + "#embedded=true&toolbar=0&navpanes=0";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }
    }
}