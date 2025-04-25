using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class IWRS_MNG_STUDY_DOCS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (!Page.IsPostBack)
                {
                    GET_DOCS();
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DOCS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DOCS_SP(ACTION: "GET_DOCS");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grd_docs.DataSource = ds;
                    grd_docs.DataBind();
                }
                else
                {
                    grd_docs.DataSource = ds;
                    grd_docs.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_DOCSNAME()
        {
            try
            {
                string fileName = DocsFile.FileName;
                string contentType = DocsFile.PostedFile.ContentType;

                byte[] fileData;
                using (Stream stream = DocsFile.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();
                    }
                }

                string DOC_NAME = "";

                DOC_NAME = txtDocName.Text;
                
                if (txtDocName.Text.Trim() == "")
                {
                    Response.Write("<script>alert('Please enetered document Name.')</script>");
                }

                else if (fileName == "")
                {
                    Response.Write("<script>alert('Please select document.')</script>");
                }
                else if (txtDocName.Text.Trim() != "" && fileName != "")
                {
                    dal_IWRS.IWRS_DOCS_SP(
                                        ACTION: "INSERT_DOCSNAME",
                                        DOCS_NAME: DOC_NAME,
                                        FileName: fileName,
                                        ContentType: contentType,
                                        fileData: fileData
                                    );

                    Response.Write("<script>alert('Document Name entered successfully.')</script>");
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        private void GET_CLEAR()
        {
            txtDocName.Text = "";
            btnSubDocs.Visible = true;
            btnUptDocs.Visible = false;
        }
        protected void btnSubDocs_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DOCS_SP(ACTION: "CHECK_DOCS", DOCS_NAME: txtDocName.Text.Trim());
                string Count = ds.Tables[0].Rows[0]["Count"].ToString();
                string Count1 = ds.Tables[1].Rows[0]["Count"].ToString();

                if (Count =="0" && Count1=="0")
                {
                    INSERT_DOCSNAME();
                    GET_DOCS();
                    GET_CLEAR();
                    
                }
                else
                {
                    Response.Write("<script>alert('Document name already Exist.Please entered another document name.')</script>");

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUptDocs_Click(object sender, EventArgs e)
        {
            DataSet ds = dal_IWRS.IWRS_DOCS_SP(ACTION: "CHECK_DOCS", DOCS_NAME: txtDocName.Text.Trim());
            string Count = ds.Tables[0].Rows[0]["Count"].ToString();
            string Count1 = ds.Tables[1].Rows[0]["Count"].ToString();

            if (Count == "0" && Count1 == "0")
            {
                UPDATE_DOCSNAME();
                GET_DOCS();
                GET_CLEAR();

                btnSubDocs.Visible = true;
                btnUptDocs.Visible = false;
                
            }
            else
            {
                Response.Write("<script>alert('Document name already Exist.Please entered another document name.')</script>");

            }
        }

        private void UPDATE_DOCSNAME()
        {
            try
            {
                try
                {
                    string fileName = DocsFile.FileName;
                    string contentType = DocsFile.PostedFile.ContentType;

                    byte[] fileData;
                    using (Stream stream = DocsFile.FileContent)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            fileData = ms.ToArray();
                        }
                    }

                    string DOC_NAME = "";

                    DOC_NAME = txtDocName.Text;
                    if (txtDocName.Text.Trim() == "")
                    {
                        Response.Write("<script>alert('Please enetered document Name.')</script>");
                    }

                    else if (fileName == "")
                    {
                        Response.Write("<script>alert('Please select document.')</script>");
                    }
                    else if (txtDocName.Text.Trim() !="" && fileName != "")
                    {
                        dal_IWRS.IWRS_DOCS_SP(
                                            ACTION: "UPDATE_DOCSNAME",
                                            DOCS_NAME: DOC_NAME,
                                            FileName: fileName,
                                            ContentType: contentType,
                                            fileData: fileData,
                                            ID: ViewState["DOC_ID"].ToString()
                                        );


                        Response.Write("<script>alert('Document Name updated successfully.')</script>");


                    }

                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_DOCSNAME(string ID)
        {
            try
            {
                dal_IWRS.IWRS_DOCS_SP(ACTION: "DELETE_DOCSNAME",
                                            ID: ID
                                        );

                Response.Write("<script>alert('Defined document name Deleted successfully.')</script>");

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT_DOCSNAME(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DOCS_SP(ACTION: "EDIT_DOCSNAME",
                                           ID: ID
                                       );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtDocName.Text = ds.Tables[0].Rows[0]["DOC_NAME"].ToString();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_docs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
               
                string ID = e.CommandArgument.ToString();

                ViewState["DOC_ID"] = ID;

                if (e.CommandName == "EDIT_DOCSNAME")
                {
                    EDIT_DOCSNAME(ID);

                    btnSubDocs.Visible = false;
                    btnUptDocs.Visible = true;
                }
                else if (e.CommandName == "DELETE_DOCSNAME")
                {
                    DELETE_DOCSNAME(ID);
                    GET_DOCS();

                    
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCALDocs_Click(object sender, EventArgs e)
        {
            
            try
            {
                GET_CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void LbtnDocsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "IWRS_REPORT_DOCS.xls";

                DataSet ds = dal_IWRS.IWRS_DOCS_SP(
                     ACTION: "IWRS_REPORTS_DOCS_AUDITRAIL"
                     );
                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}