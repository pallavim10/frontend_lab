using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace CTMS
{
    public partial class ViewProjects : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction Comfun = new CommonFunction.CommonFunction();
        int projectid;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetProjects();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        public void GetProjects()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.GetSetPROJECTDETAILS(Action: "Get_All_Project");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdprojects.DataSource = ds;
                    grdprojects.DataBind();
                }
                else
                {
                    grdprojects.DataSource = null;
                    grdprojects.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        public string db_name()
        {
            string dbname = "";
            try
            {
                dal = new DAL();
                DataSet ds1 = dal.AddUserProfile(Action: "CheckDBName", PROJECTID: projectid.ToString());
                string CON = ds1.Tables[0].Rows[0]["ConnectionString"].ToString();
                string[] parts = CON.Split(';');
               
                for (int i = 0; i < parts.Length; i++)
                {
                    string part = parts[i].Trim();

                    if (part.StartsWith("Initial Catalog="))
                    {
                        dbname = part.Replace("Initial Catalog=", "");

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
            return dbname;
        }

        protected void grdprojects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                dal = new DAL();

                projectid = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "EDIT")
                {
                    Response.Redirect("ADD_PROJECT_MASTER.aspx?Action=" + "Update" + "&ProjectId=" + projectid);
                }
                else if (e.CommandName == "DELETE")
                {
                    try
                    {   
                       string CHILDDBNAME= db_name();
                     

                        DataSet ds = dal.GetSetPROJECTDETAILS(Action: "Delete_Project", Project_ID: projectid, ENTEREDBY: Session["User_ID"].ToString());
                        ds=dal.GetSetPROJECTDETAILS(Action: "DELETE_Indic", Project_ID: projectid,
                            ENTEREDBY: Session["User_ID"].ToString(),
                            IPADDRESS : Comfun.GetIpAddress()
                            );
                        GetProjects();
                    }
                    catch (Exception ex)
                    {
                        lblErrorMsg.Text = ex.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }


        protected void grdprojects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string PROJECTNAME = dr["PROJNAME"].ToString();
                    if (Session["PROJECTIDTEXT"] != null)
                    {
                        if (PROJECTNAME == Session["PROJECTIDTEXT"].ToString())
                        {
                            LinkButton lbtnupdate = (LinkButton)e.Row.FindControl("lbtnupdate");
                            lbtnupdate.Visible = true;
                        }
                        else
                        {
                            LinkButton lbtnupdate = (LinkButton)e.Row.FindControl("lbtnupdate");
                            lbtnupdate.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdprojects_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
                {
                    //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {
                    //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }
        protected void lbExportProject_Click(object sender, EventArgs e)
        {
            try
            {
                Project_Master(header: "Project Master", Action: "GET_PROJECTDETAILS");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Project_Master(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.Export_Data_SP(Action: Action, PRODUCTID: PRODUCT_ID, SUBCLASSID: SUBCLASS_ID);

                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        


    }
}