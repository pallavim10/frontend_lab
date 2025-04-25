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
    public partial class CTMS_TimeLineComment : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtComment.Text = Request.QueryString["Comment"].ToString();

                    DataSet ds1 = dal.Budget_SP(Action: "single_SubTask", Task_ID: Request.QueryString["TaskID"].ToString(), Sub_Task_ID: Request.QueryString["SubTaskID"].ToString());
                    lbl_Task_Name.Text = ds1.Tables[0].Rows[0]["Sub_Task_Name"].ToString();

                    if (Request.QueryString["ID"] != null)
                    {
                        hfdID.Value = Request.QueryString["ID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dal.Budget_SP(Action: "update_TimeLine_Comment", Task_ID: Request.QueryString["TaskID"].ToString(), Sub_Task_ID: Request.QueryString["SubTaskID"].ToString(), INVID: Request.QueryString["INVID"].ToString(), Others: txtComment.Text, Project_Id: Session["PROJECTID"].ToString(), ID: hfdID.Value);

                Response.Write("<script> alert('Comment Updated successfully.')</script>");
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}