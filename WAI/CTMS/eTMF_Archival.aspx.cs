using System;
using System.Web;
using System.Web.UI;
using System.Data;
using PPT;
using System.IO;
using Ionic.Zip;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_Archival : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();

        DataSet dsUsers = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!this.IsPostBack)
                {
                    if (Session["Unblind"].ToString() == "Unblinded")
                    {
                        divBlinded.Visible = true;
                        divUnblinded.Visible = true;
                    }
                    else
                    {
                        divBlinded.Visible = true;
                        divUnblinded.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BtnExpArchival_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chkBlinded.Checked && !chkUnblinded.Checked)
                {
                    Response.Write("<script> alert('Please select Unblinding Type.')</script>");
                }
                else
                {
                    ExportZip();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportZip()
        {
            try
            {
                string BLINDED = chkBlinded.Checked.ToString();
                string UNBLINDED = chkUnblinded.Checked.ToString();

                char[] invalidChars = Path.GetInvalidFileNameChars();

                DataSet dsPath = dal_eTMF.eTMF_SPECS_SP(ACTION: "GET_Path_Zip", BLINDED: BLINDED, UNBLINDED: UNBLINDED);

                string MainFolder = "eTMF_Zip";

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(Server.MapPath("~/eTMF_Zip/")))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(Server.MapPath("~/eTMF_Zip/"));
                }
                else
                {
                    DirectoryInfo dirOld = new DirectoryInfo(Server.MapPath("~/eTMF_Zip/"));
                    foreach (DirectoryInfo dir in dirOld.GetDirectories())
                    {
                        foreach (FileInfo file in dir.GetFiles())
                        {
                            file.Delete();
                        }

                        if (dir.Name != "Other details")
                            dir.Delete(true);
                    }

                    Directory.CreateDirectory(Server.MapPath("~/eTMF_Zip/"));
                }

                if (dsPath.Tables.Count > 0 && dsPath.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drPath in dsPath.Tables[0].Rows)
                    {
                        string zonePath = MakeValidFileName(drPath["ZonePath"].ToString().Trim());
                        string sectionPath = MakeValidFileName(drPath["SectionPath"].ToString().Trim());
                        string artifactPath = MakeValidFileName(drPath["ArtifactPath"].ToString().Trim());

                        string mainPath = @"" + MainFolder + "";
                        string zPath = Path.Combine(mainPath, zonePath);
                        string sPath = Path.Combine(zPath, sectionPath);
                        string aPath = Path.Combine(sPath, artifactPath);
                        string fullPath = aPath.Trim();

                        DirectoryInfo folderPath = new DirectoryInfo(Server.MapPath(@"~\" + fullPath.Trim()));
                        if (!Directory.Exists(fullPath))
                        {
                            folderPath.Create();
                        }

                        string DocId = drPath["DocId"].ToString();

                        DataSet dsFiles = dal_eTMF.eTMF_SPECS_SP(ACTION: "GET_Files_Zip", ID: DocId, BLINDED: BLINDED, UNBLINDED: UNBLINDED);

                        if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drFiles in dsFiles.Tables[0].Rows)
                            {
                                string SysFileName = drFiles["SysFileName"].ToString();
                                string UploadFileName = drFiles["UploadFileName"].ToString();

                                string oldPaths = Path.Combine("eTMF_Docs", SysFileName);
                                string newPaths = Path.Combine(fullPath, UploadFileName);

                                DirectoryInfo oldPathAndName = new DirectoryInfo(Server.MapPath(@"~\" + oldPaths));
                                DirectoryInfo newPathAndName = new DirectoryInfo(Server.MapPath(@"~\" + newPaths));

                                if (File.Exists(oldPathAndName.ToString()) && !File.Exists(newPathAndName.ToString()))
                                {
                                    File.Copy(oldPathAndName.ToString(), newPathAndName.ToString());
                                }

                                File.SetAttributes(newPathAndName.ToString(), File.GetAttributes(newPathAndName.ToString()) & ~FileAttributes.ReadOnly);

                            }
                        }
                    }
                }

                DirectoryInfo METATDATAfolderPath = new DirectoryInfo(Server.MapPath(@"~\" + MainFolder + "\\Other details"));
                if (!Directory.Exists(Server.MapPath(@"~\" + MainFolder + "\\Other details")))
                {
                    METATDATAfolderPath.Create();
                }

                string filePath = Server.MapPath(@"~\eTMF_Zip\\Other details");

                DataSet dsMetaData = dal_eTMF.eTMF_SPECS_SP(ACTION: "GET_MetaData", BLINDED: BLINDED, UNBLINDED: UNBLINDED);
                Multiple_Export_Excel.SaveExcel(dsMetaData, "eTMF_METADATA", Page.Response, Server.MapPath(@"~\eTMF_Zip\\Other details"));
                File.SetAttributes(filePath, FileAttributes.ReadOnly);

                DataSet dsAUDITTRAIL = dal_eTMF.eTMF_SPECS_SP(ACTION: "GET_AUDITTRAIL", BLINDED: BLINDED, UNBLINDED: UNBLINDED);
                Multiple_Export_Excel.SaveExcel(dsAUDITTRAIL, "eTMF_AUDITTRAIL", Page.Response, Server.MapPath(@"~\eTMF_Zip\\Other details"));
                File.SetAttributes(filePath, FileAttributes.ReadOnly);

                string ZipFilename = "eTMF_" + DateTime.Now.ToString("ddMMMyyyyHHmm") + ".zip";

                try
                {
                    using (var zip = new ZipFile())
                    {
                        zip.Password = txtSetPassword.Text;

                        foreach (var item in Directory.GetDirectories(Server.MapPath(@"~\eTMF_Zip")))
                        {
                            string folderName = new DirectoryInfo(item).Name;
                            zip.AddDirectory(item, folderName);
                        }

                        zip.Save(Server.MapPath(@"~\eTMF_Docs\" + ZipFilename + ""));

                        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace(Request.RawUrl.ToString(), "/eTMF_Docs/" + ZipFilename + ""));

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Archival file exported successfully.');window.location.href = '" + HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace(Request.RawUrl.ToString(), "/eTMF_Docs/" + ZipFilename + "") + "'; ", true);

                    }
                }
                catch (Exception ex)
                { }

                //System.IO.Directory.Delete(Server.MapPath("~/eTMF_Zip/"));

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private static string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");

        }
    }
}
