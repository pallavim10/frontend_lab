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

namespace CTMS
{
    public partial class Comm_NewMail : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction comfunc = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    fill_Project();
                    fill_Dept();

                    DataSet dss = dal.Communication_SP(Action: "Get_Un_Pwd", UserID: Session["User_ID"].ToString());

                    txtFromID.Text = dss.Tables[0].Rows[0]["Mail_Username"].ToString();
                    hfPWD.Value = dss.Tables[0].Rows[0]["Mail_Password"].ToString();

                    if (Request.QueryString["ComId"] != null)
                    {
                        DataSet ds = dal.Communication_SP(Action: "Inbox_View", ID: Request.QueryString["ComId"].ToString());

                        if (Request.QueryString["ComType"] == "Reply")
                        {
                            txtToID.Text = ds.Tables[0].Rows[0]["From"].ToString();
                            txtSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                            txtSubject.ReadOnly = true;
                            txtBody.Text = "<br /><br /><br />---------------------------------------------------------------------------------------------------------------------------------------------------------------------" + ds.Tables[0].Rows[0]["Body"].ToString();
                        }
                        else if (Request.QueryString["ComType"] == "ReplyAll")
                        {
                            txtToID.Text = ds.Tables[0].Rows[0]["From"].ToString();
                            txtCcID.Text = ds.Tables[0].Rows[0]["Cc"].ToString();
                            txtBccID.Text = ds.Tables[0].Rows[0]["Bcc"].ToString();
                            txtSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                            txtSubject.ReadOnly = true;
                            txtBody.Text = "<br /><br /><br />---------------------------------------------------------------------------------------------------------------------------------------------------------------------" + ds.Tables[0].Rows[0]["Body"].ToString();
                        }
                        else if (Request.QueryString["ComType"] == "Forward")
                        {
                            txtSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                            txtSubject.ReadOnly = true;
                            txtBody.Text = ds.Tables[0].Rows[0]["Body"].ToString();
                        }


                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            Drp_Type.SelectedValue = ds.Tables[1].Rows[0]["Type"].ToString();
                            Drp_Refer.SelectedValue = ds.Tables[1].Rows[0]["Reference"].ToString();

                            list_Dept.ClearSelection();

                            string[] Dept = ds.Tables[1].Rows[0]["Department"].ToString().Split(',');

                            foreach (string D in Dept)
                            {
                                if (D != "")
                                {
                                    foreach (ListItem item in list_Dept.Items)
                                    {
                                        if (item.Text == D)
                                        {
                                            item.Selected = true;
                                        }
                                    }
                                }
                            }

                            fill_Event();

                            ddlEvent.SelectedValue = ds.Tables[1].Rows[0]["Event"].ToString();
                        }

                    }
                }
                fileAttach.Attributes["multiple"] = "multiple";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendMail();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Comm_NewMail.aspx", false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SendMail()
        {
            try
            {
                string ToEmailAddress, CCEmailAddress, BCCEmailAddres, subject, body, Importance;
                bool Flag = false;

                ToEmailAddress = txtToID.Text;
                CCEmailAddress = txtCcID.Text;
                BCCEmailAddres = txtBccID.Text;
                subject = txtSubject.Text;
                subject = txtSubject.Text;
                body = txtBody.Text;
                Importance = Drp_Importance.SelectedValue;
                Flag = chkFlag.Checked;

                DataTable DtAttach = new DataTable();
                DtAttach.Columns.Add("FileName", typeof(System.String));
                DtAttach.Columns.Add("ContentType", typeof(System.String));
                DtAttach.Columns.Add("Data", typeof(System.Byte[]));

                if (fileAttach.HasFile)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFile uploadedFile = Request.Files[i];
                        string filename = Path.GetFileName(uploadedFile.FileName);
                        string contentType = uploadedFile.ContentType;

                        using (Stream fs = uploadedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                DtAttach.Rows.Add(filename, contentType, bytes);
                            }
                        }
                    }
                }

                DataTable dtInsertedReturns = new DataTable();

                dtInsertedReturns = Insert_SendMail();

                string CommUNIQUENUM = dtInsertedReturns.Rows[0]["UNIQUEID"].ToString();
                string CommID = dtInsertedReturns.Rows[0]["ID"].ToString();

                foreach (DataRow dr in DtAttach.Rows)
                {
                    byte[] bytes = (byte[])dr["Data"];
                    string fileName = dr["FileName"].ToString();
                    string ContentType = dr["ContentType"].ToString();
                    MemoryStream ms = new MemoryStream(bytes);

                    Insert_Attachments(CommID, fileName, ContentType, bytes);
                }

                if (!subject.Contains("[Comn#"))
                {
                    subject = subject + "     [Comn#" + CommUNIQUENUM + "]";
                }

                bool Success = comfunc.Communication_SendMail(txtFromID.Text, hfPWD.Value, ToEmailAddress, CCEmailAddress, BCCEmailAddres, subject, body, DtAttach, Importance);

                dal.Communication_SP(Action: "Update_Status", Success: Success, ID: CommID);

                MAILSAVETO_SENTFOLDER();

                Response.Redirect("Comm_NewMail.aspx", false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void MAILSAVETO_SENTFOLDER()
        { 
        
        }

        private void fill_Project()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project.DataSource = ds.Tables[0];
                Drp_Project.DataValueField = "Project_ID";
                Drp_Project.DataTextField = "PROJNAME";
                Drp_Project.DataBind();
                Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
                if (Session["PROJECTID"] != null)
                {
                    Drp_Project.Items.FindByValue(Session["PROJECTID"].ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Dept()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Dept");

                list_Dept.Items.Clear();
                list_Dept.DataSource = ds;
                list_Dept.DataTextField = "Dept_Name";
                list_Dept.DataValueField = "Dept_ID";
                list_Dept.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Event()
        {
            try
            {
                string Department = null;
                foreach (ListItem item in list_Dept.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Department == null)
                        {
                            Department = item.Value;
                        }
                        else
                        {
                            Department += "," + item.Value;
                        }
                    }
                }

                DataSet ds = dal.Budget_SP(Action: "Get_Event", Dept_Name: Department);

                ddlEvent.DataSource = ds;
                ddlEvent.DataValueField = "Event";
                ddlEvent.DataBind();

                ddlEvent.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlEvent.Items.Insert(1, new ListItem("Not In List", "Not In List"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private DataTable Insert_SendMail()
        {
            DataTable ReturnValue = new DataTable();
            try
            {
                string FromID = null, ToID = null, CcID = null, BccID = null, Subject = null, Body = null, UserID = null, PROJECTID = null, Type = null,
                    Nature = null, Notes = null, Department = null, Reference = null, Event = null, Importance = null;
                bool Flag = false;

                FromID = txtFromID.Text;
                ToID = txtToID.Text;
                CcID = txtCcID.Text;
                BccID = txtBccID.Text;
                Subject = txtSubject.Text;
                Body = txtBody.Text;
                Flag = chkFlag.Checked;
                if (Drp_Importance.SelectedIndex != 0)
                {
                    Importance = Drp_Importance.SelectedValue;
                }
                UserID = Session["User_ID"].ToString();
                PROJECTID = Session["PROJECTID"].ToString();
                Nature = Drp_Nature.SelectedValue;
                Notes = txtNote.Text;
                Type = Drp_Type.SelectedValue;
                foreach (ListItem item in list_Dept.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Department == null)
                        {
                            Department = item.Text;
                        }
                        else
                        {
                            Department += "," + item.Text;
                        }
                    }
                }
                Reference = Drp_Refer.SelectedValue;
                Event = ddlEvent.SelectedValue;
                DataSet ds;

                string UniqueID = null;
                if (Subject.Contains("[Comn#"))
                {
                    UniqueID = Between(Subject, "[Comn#", "]");
                }

                if (Request.QueryString["ComID"] != null)
                {
                    ds = dal.Communication_SP(Action: "Insert_ReplyMail",
                    FromID: FromID,
                    ToID: ToID,
                    CcID: CcID,
                    BccID: BccID,
                    Subject: Subject,
                    Body: Body,
                    UserID: UserID,
                    PROJECTID: PROJECTID,
                    Importance: Importance,
                    ParentComm: Request.QueryString["ComId"].ToString(),
                    UNIQUEID: UniqueID
                    );

                    if (chkEvent.Checked)
                    {
                        dal.Communication_SP(Action: "Insert_ReplyMail_Event",
                        UNIQUEID: ds.Tables[0].Rows[0]["UNIQUEID"].ToString(),
                        UserID: UserID,
                        PROJECTID: PROJECTID,
                        Type: Type,
                        Nature: Nature,
                        Notes: Notes,
                        Department: Department,
                        Reference: Reference,
                        Event: Event
                        );
                    }
                }
                else
                {
                    ds = dal.Communication_SP(Action: "Insert_SendMail",
                    FromID: FromID,
                    ToID: ToID,
                    CcID: CcID,
                    BccID: BccID,
                    Subject: Subject,
                    Body: Body,
                    UserID: UserID,
                    PROJECTID: PROJECTID,
                    Importance: Importance
                    );

                    if (chkEvent.Checked)
                    {
                        dal.Communication_SP(Action: "Insert_SendMail_Event",
                        UNIQUEID: ds.Tables[0].Rows[0]["UNIQUEID"].ToString(),
                        UserID: UserID,
                        PROJECTID: PROJECTID,
                        Type: Type,
                        Nature: Nature,
                        Notes: Notes,
                        Department: Department,
                        Reference: Reference,
                        Event: Event
                        );
                    }

                    //ds = dal.Communication_SP(Action: "Insert_SendMail",
                    //FromID: FromID,
                    //ToID: ToID,
                    //CcID: CcID,
                    //BccID: BccID,
                    //Subject: Subject,
                    //Body: Body,
                    //UserID: UserID,
                    //PROJECTID: PROJECTID,
                    //Type: Type,
                    //Nature: Nature,
                    //Notes: Notes,
                    //Department: Department,
                    //Reference: Reference,
                    //Event: Event,
                    //Importance: Importance
                    //);
                }


                ReturnValue = ds.Tables[0];
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return ReturnValue;
        }

        private void Insert_Attachments(string CommId, string FileName, string ContentType, byte[] Data)
        {
            try
            {
                dal.Communication_SP(Action: "Insert_Attachment", Comm_ID: CommId, FileName: FileName, ContentType: ContentType, Data: Data);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void list_Dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Event();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        public string Between(string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }
    }
}