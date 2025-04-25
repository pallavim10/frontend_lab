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
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace CTMS
{
    public partial class Comm_Outbox : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction comfunc = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Get_Outbox();
            }
        }

        private void Get_Outbox()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "Get_Outbox", UserID: Session["User_ID"].ToString(), PROJECTID: Session["PROJECTID"].ToString(), Reference: txtAutoCompText.Text);

                repeatOutbox.DataSource = ds;
                repeatOutbox.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatOutbox_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();

                if (e.CommandName == "Read")
                {
                    dal.Communication_SP(Action: "Read", ID: ID);
                }
                else if (e.CommandName == "UnRead")
                {
                    dal.Communication_SP(Action: "UnRead", ID: ID);
                }
                else if (e.CommandName == "DeleteMail")
                {
                    dal.Communication_SP(Action: "Delete", ID: ID);
                }
                else if (e.CommandName == "Open")
                {
                    Response.Redirect("Comm_InboxView.aspx?ComID=" + ID);
                }
                else if (e.CommandName == "Flag")
                {
                    dal.Communication_SP(Action: "Flag", ID: ID);
                }
                else if (e.CommandName == "UnFlag")
                {
                    dal.Communication_SP(Action: "UnFlag", ID: ID);
                }

                Get_Outbox();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatOutbox_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string Read = row["Read"].ToString();

                    HtmlGenericControl divMailItem = e.Item.FindControl("divMailItem") as HtmlGenericControl;
                    LinkButton lbtnUnRead = (LinkButton)e.Item.FindControl("lbtnUnRead");
                    LinkButton lbtnRead = (LinkButton)e.Item.FindControl("lbtnRead");

                    if (Read == "True")
                    {
                        lbtnRead.Visible = false;
                        lbtnUnRead.Visible = true;
                    }
                    else
                    {
                        string CLASS = divMailItem.Attributes["class"].ToString();
                        CLASS = CLASS + " brd-1px-blueimp fontBold";
                        divMailItem.Attributes.Add("class", CLASS);
                        lbtnRead.Visible = true;
                        lbtnUnRead.Visible = false;
                    }

                    string Flag = row["Flag"].ToString();
                    LinkButton lbtnUnFlag = (LinkButton)e.Item.FindControl("lbtnUnFlag");
                    LinkButton lbtFlag = (LinkButton)e.Item.FindControl("lbtFlag");

                    if (Flag == "True")
                    {
                        lbtnUnFlag.Visible = true;
                        lbtFlag.Visible = false;
                    }
                    else
                    {
                        lbtFlag.Visible = true;
                        lbtnUnFlag.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        //Method for get  userid from session    
        [WebMethod(EnableSession = true)]
        public static string GetUserId()
        {
            try
            {
                return HttpContext.Current.Session["User_ID"].ToString();
            }
            catch (Exception ex)
            {
                //  return ex.Message.ToString();
                throw;
            }
        }

        //Method for get  Project ID from session    
        [WebMethod(EnableSession = true)]
        public static string GetProjectId()
        {
            try
            {
                return HttpContext.Current.Session["PROJECTID"].ToString();
            }
            catch (Exception ex)
            {
                //return ex.Message.ToString();
                throw;
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<string> GetSearch(string prefixText)
        {
            DAL dal = new DAL();
            string ProjectID = GetProjectId();
            string UserId = GetUserId();
            DataSet ds = dal.Communication_SP(Action: "Get_Mail_Prefix", UserID: UserId, PROJECTID: ProjectID, Reference: prefixText);
            List<string> Output = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                Output.Add(ds.Tables[0].Rows[i][0].ToString());
            return Output;
        }

        protected void txtAutoCompText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Get_Outbox();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}