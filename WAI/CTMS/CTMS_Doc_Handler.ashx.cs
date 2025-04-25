using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;
using System;
using CTMS.CommonFunction;

namespace CTMS
{
    /// <summary>
    /// Summary description for CTMS_Doc_Handler
    /// </summary>
    public class CTMS_Doc_Handler : IHttpHandler
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            try
            {
                string ID = context.Request["ID"];
                byte[] bytes;
                string fileName, contentType;
                
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "Download", ID: ID);

                bytes = (byte[])ds.Tables[0].Rows[0]["Data"];
                contentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                fileName = ds.Tables[0].Rows[0]["FileName"].ToString();

                context.Response.Clear();
                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = contentType;
                context.Response.AppendHeader("Content-Disposition", "Inline; filename=" + fileName);
                context.Response.BinaryWrite(bytes);
                context.Response.Flush();
                context.Response.End();
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message.ToString());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}