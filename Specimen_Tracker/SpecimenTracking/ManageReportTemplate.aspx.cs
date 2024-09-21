using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class ManageReportTemplate : System.Web.UI.Page
    {
        DAL_SETUP Dal_Setup = new DAL_SETUP();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_SHIPMENT_MANIFEST();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void GET_SHIPMENT_MANIFEST()
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_SHIPMENT_MANIFEST_SP(
                        ACTION: "GET_SHIPMENT_MANIFEST"
                        );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GrdShipment.DataSource = ds.Tables[0];
                    GrdShipment.DataBind();
                }
                else
                {
                    GrdShipment.DataSource = null;
                    GrdShipment.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void lbtnUpload_Click(object sender, EventArgs e)
        {
            string fileName = Manifestfilename.FileName;
            if (fileName.Trim() !="")
            {
                UPLOAD_SHIPMENT_MANIFEST();
            }
           
            GET_SHIPMENT_MANIFEST();
        }
        private void UPLOAD_SHIPMENT_MANIFEST()
        {
            try
            {

                string fileName = Manifestfilename.FileName;
                string contentType = Manifestfilename.PostedFile.ContentType;
                string fileExtension = Path.GetExtension(fileName).ToLower();
                int fileSize = Manifestfilename.PostedFile.ContentLength;

                byte[] fileData;
                using (Stream stream = Manifestfilename.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();

                    }
                }
                if (fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".docm" || fileExtension == ".xlx" || fileExtension == "..xlsx" || fileExtension == ".csv")
                {
                    DataSet ds = Dal_Setup.SETUP_SHIPMENT_MANIFEST_SP(ACTION: "UPLOAD_SHIPMENT_MANIFEST",
                        FILENAME: fileName,
                        CONTENT_TYPE: contentType,
                        DATA_TYPE: fileData,
                        SIZE: fileSize.ToString()
                        );

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'file Uploaded Successfully.', 'success');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Select Word File Only.', 'warning');", true);
                    
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Manifestfilename.Attributes.Clear();
        }

        protected void GrdShipment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            if(e.CommandName.ToString() == "Download")
            {
                DOWNLOAD(ID);
            }
        }

        private void DOWNLOAD(string ID)
        {
            try
            {
                string FileName, ContentType;
                byte[] fileData;
                DataSet ds = Dal_Setup.SETUP_SHIPMENT_MANIFEST_SP(ACTION: "DOWNLOAD", ID: ID);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                        ContentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                        fileData = (byte[])ds.Tables[0].Rows[0]["DATA"];
                        Response.Clear();
                        Response.ContentType = ContentType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                        Response.BinaryWrite(fileData);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}