using PPT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class NIWRS_SETUP_QUES : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSubjectID.Text = Session["SUBJECTTEXT"].ToString();
            }
               
        }

        protected void btnSubmitQues_Click(object sender, EventArgs e)
        {
            try
            {
                Insert_Ques();

                if (UserManualFile.HasFile)
                {
                    Insert_UserManual();

                    Response.Write("<script>alert('User Manual Upload Successfully')</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Insert_Ques()
        {
            try
            {
               
                
                string QUESTION = "", QUECODE = "", ANS = "";

                QUESTION = "Participant id must be.";
                QUECODE = "SUBJECTTEXT";
                ANS = txtSubjectID.Text;

                dal_IWRS.IWRS_SET_OPTION_SP(
                    ACTION: "INSERT_QUES",
                    QUESTION: QUESTION,
                    QUECODE: QUECODE,
                    ANS: ANS,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    
                    );

                Session["SUBJECTTEXT"] = txtSubjectID.Text;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Insert_UserManual()
        {
            try
            {
                string fileName = UserManualFile.FileName;
                string contentType = UserManualFile.PostedFile.ContentType;
                byte[] fileData;
                using (Stream stream = UserManualFile.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();
                    }
                }

                string QUESTION = "", QUECODE = "", ANS = "";

                QUESTION = "User manual  must be.";
                QUECODE = "USERMANUAL";
                ANS = txtSubjectID.Text;

                dal_IWRS.IWRS_SET_OPTION_SP(
                    ACTION: "INSERT_QUES",
                    QUESTION: QUESTION,
                    QUECODE: QUECODE,
                    ANS: ANS,
                    ENTEREDBY: Session["USER_ID"].ToString(),
                    FileName: fileName,
                    ContentType: contentType,
                    fileData: fileData
                    );
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnCancelQues_Click(object sender, EventArgs e)
        {
            Response.Redirect("NIWRS_SETUP_QUES.aspx");
        }
    }
}