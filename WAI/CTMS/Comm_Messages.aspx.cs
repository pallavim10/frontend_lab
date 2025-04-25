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

namespace CTMS
{
    public partial class Comm_Messages : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction comfunc = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Get_Inbox();
                Get_Sent();
                Get_Delete();
            }

        }

        private void Get_Inbox()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "Get_Inbox", UserID: Session["User_ID"].ToString(), PROJECTID: Session["PROJECTID"].ToString());

                repeatInbox.DataSource = ds;
                repeatInbox.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Sent()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "Get_Sent", UserID: Session["User_ID"].ToString(), PROJECTID: Session["PROJECTID"].ToString());

                repeatSent.DataSource = ds;
                repeatSent.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Delete()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "Get_Delete", UserID: Session["User_ID"].ToString(), PROJECTID: Session["PROJECTID"].ToString());

                repeatDelete.DataSource = ds;
                repeatDelete.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatInbox_ItemCommand(object sender, RepeaterCommandEventArgs e)
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

                Get_Inbox();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatInbox_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                        CLASS = CLASS + " brd-1px-blueimp";
                        divMailItem.Attributes.Add("class", CLASS);
                        lbtnRead.Visible = true;
                        lbtnUnRead.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void repeatSent_ItemCommand(object sender, RepeaterCommandEventArgs e)
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

                Get_Inbox();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatSent_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void repeatDelete_ItemCommand(object sender, RepeaterCommandEventArgs e)
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
                else if (e.CommandName == "RestoreMail")
                {
                    dal.Communication_SP(Action: "Restore", ID: ID);
                }
                else if (e.CommandName == "Open")
                {
                    Response.Redirect("Comm_InboxView.aspx?ComID=" + ID);
                }

                Get_Inbox();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatDelete_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                        CLASS = CLASS + " brd-1px-blueimp";
                        divMailItem.Attributes.Add("class", CLASS);
                        lbtnRead.Visible = true;
                        lbtnUnRead.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

    }
}