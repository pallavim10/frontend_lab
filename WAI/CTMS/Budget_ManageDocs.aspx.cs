using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class Budget_ManageDocs : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetDoc_SubTaskName();
                    Getdata();
                    GetDocument_Ref();
                    GET_BD_DOCS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GetDoc_SubTaskName()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GetDoc_SubTaskName", Task_ID: Request.QueryString["Task_ID"].ToString(), Sub_Task_ID: Request.QueryString["Sub_Task_ID"].ToString());
                txtTask.Text = ds.Tables[0].Rows[0]["Task_Name"].ToString();
                txtSubTask.Text = ds.Tables[0].Rows[0]["Sub_Task_Name"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SelectDocument(string ID)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "SelectDocument", ID: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRefId.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();
                    txtDocName.Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                    ddlAutoReplace.SelectedItem.Text = ds.Tables[0].Rows[0]["AutoReplace"].ToString();
                    txtUniqueRefId.Text = ds.Tables[0].Rows[0]["UniqueRefNo"].ToString();

                    ddlDocName.SelectedIndex = 0;
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    repeatDocType.DataSource = ds.Tables[1];
                    repeatDocType.DataBind();
                }

                btnupdate.Visible = true;
                btnsubmit.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetDocument_Ref()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GetDocument_Ref");
                repeatDocType.DataSource = ds;
                repeatDocType.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Getdata()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GetDocument", Task_ID: Request.QueryString["Task_ID"].ToString(), Sub_Task_ID: Request.QueryString["Sub_Task_ID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdData.DataSource = ds.Tables[0];
                    grdData.DataBind();
                }
                else
                {
                    grdData.DataSource = null;
                    grdData.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Insert_Documents()
        {
            try
            {
                DataSet ds = new DataSet();

                if (ddlDocName.SelectedItem.Text == "None")
                {
                    ds = dal_eTMF.eTMF_SET_SP(
                    ACTION: "InsertDocument",
                    RefNo: txtRefId.Text,
                    DocName: txtDocName.Text,
                    AutoReplace: ddlAutoReplace.SelectedValue,
                    Task_ID: Request.QueryString["Task_ID"].ToString(),
                    Sub_Task_ID: Request.QueryString["Sub_Task_ID"].ToString(),
                    UniqueRefNo: txtUniqueRefId.Text
                    );
                    string DocID = ds.Tables[0].Rows[0]["ID"].ToString();

                    for (int a = 0; a < repeatDocType.Items.Count; a++)
                    {
                        string DocTypeID = ((HiddenField)repeatDocType.Items[a].FindControl("hdneMTFID")).Value.ToString();
                        string RefNum = ((TextBox)repeatDocType.Items[a].FindControl("txteTMFRefNum")).Text.ToString();

                        dal_eTMF.eTMF_SET_SP(ACTION: "InsertDocument_Ref", DocID: DocID, DocTypeId: DocTypeID, RefNo: RefNum);
                    }
                }
                else
                {
                    dal_eTMF.eTMF_SET_SP(ACTION: "Update_BD_DOC",
                    Task_ID: Request.QueryString["Task_ID"].ToString(),
                    Sub_Task_ID: Request.QueryString["Sub_Task_ID"].ToString(),
                    ID: ddlDocName.SelectedValue,
                    UniqueRefNo: txtUniqueRefId.Text
                    );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Update_Document()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(
                ACTION: "UpdateDocument",
                RefNo: txtRefId.Text,
                DocName: txtDocName.Text,
                AutoReplace: ddlAutoReplace.SelectedValue,
                Task_ID: Request.QueryString["Task_ID"].ToString(),
                Sub_Task_ID: Request.QueryString["Sub_Task_ID"].ToString(),
                ID: Session["ID"].ToString(),
                UniqueRefNo: txtUniqueRefId.Text
                );
                string DocID = Session["ID"].ToString();

                for (int a = 0; a < repeatDocType.Items.Count; a++)
                {
                    string DocTypeID = ((HiddenField)repeatDocType.Items[a].FindControl("hdneMTFID")).Value.ToString();
                    string RefNum = ((TextBox)repeatDocType.Items[a].FindControl("txteTMFRefNum")).Text.ToString();

                    dal_eTMF.eTMF_SET_SP(ACTION: "InsertDocument_Ref", DocID: DocID, DocTypeId: DocTypeID, RefNo: RefNum);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Insert_Documents();
                txtRefId.Text = "";
                txtDocName.Text = "";
                ddlAutoReplace.SelectedValue = "0";
                txtUniqueRefId.Text = "";
                ddlDocName.SelectedIndex = 0;
                GetDocument_Ref();
                Getdata();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Document();
                GetDocument_Ref();
                Getdata();
                txtRefId.Text = "";
                txtDocName.Text = "";
                ddlAutoReplace.SelectedValue = "0";
                txtUniqueRefId.Text = "";
                ddlDocName.SelectedIndex = 0;
                btnsubmit.Visible = true;
                btnupdate.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            GetDocument_Ref();
            Getdata();
            txtRefId.Text = "";
            txtDocName.Text = "";
            ddlAutoReplace.SelectedValue = "0";
            txtUniqueRefId.Text = "";
            btnsubmit.Visible = true;
            btnupdate.Visible = false;
        }

        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                if (e.CommandName == "EditModule")
                {
                    SelectDocument(id);
                }
                else if (e.CommandName == "DeleteModule")
                {
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "DeleteDocument", ID: id);
                    Getdata();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlDocName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "SelectDocument", ID: ddlDocName.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRefId.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();
                    txtDocName.Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                    ddlAutoReplace.SelectedItem.Text = ds.Tables[0].Rows[0]["AutoReplace"].ToString();
                    txtUniqueRefId.Text = ds.Tables[0].Rows[0]["UniqueRefNo"].ToString();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    repeatDocType.DataSource = ds.Tables[1];
                    repeatDocType.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_BD_DOCS()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GET_BD_DOCS_OPEN");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDocName.DataSource = ds;
                    ddlDocName.DataTextField = "DocName";
                    ddlDocName.DataValueField = "ID";
                    ddlDocName.DataBind();
                    ddlDocName.Items.Insert(0, new ListItem("None", "0"));
                }
                else
                {
                    ddlDocName.Items.Clear();
                    ddlDocName.Items.Insert(0, new ListItem("None", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}