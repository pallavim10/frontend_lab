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

namespace CTMS
{
    public partial class Train_StudyMaterial : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    get_Doc();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Doc()
        {
            try
            {
                if (!IsPostBack)
                {
                    ID = Request.QueryString["ID"].ToString();

                    DataSet ds= dal.Training_SP(Action: "get_Doc", Section_ID: ID);
                    gvMaterial.DataSource = ds.Tables[0];
                    gvMaterial.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                DownloadFile(ID);
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

                DataSet ds = dal.Training_SP(Action: "Download", ID: ID);

                bytes = (byte[])ds.Tables[0].Rows[0]["Data"];
                contentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                fileName = ds.Tables[0].Rows[0]["FileName"].ToString();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "Inline; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }

        }
    }
}