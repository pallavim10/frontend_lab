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
    public partial class View_EmpDetails : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction Comfun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    get_EmpDetails();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_EmpDetails()
        {
            try
            {
                DataSet ds = dal.EmpMaster_SP(Action: "VIEW");
                gvEmp.DataSource = ds.Tables[0];
                gvEmp.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                if (e.CommandName == "Edit1")
                {
                    Response.Redirect("Emp_Master.aspx?ID=" + id + "");
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Emp(id);
                    get_EmpDetails();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }            
        }

        private void delete_Emp(string ID)
        {
            try
            {
                dal.EmpMaster_SP(Action: "DELETE", ID: ID, ENTEREDBY: Session["User_ID"].ToString(),
                    IPADDRESS :Comfun.GetIpAddress());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteEmp = (e.Row.FindControl("lbtndeleteEmp") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteEmp.Visible = false;
                    }
                    else
                    {
                        lbtndeleteEmp.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvEmp_PreRender(object sender, EventArgs e)
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

        protected void lbEmployeeMaster_Click(object sender, EventArgs e)
        {
            try
            {
                Emplopyee_Master(header: "Emplopyee Master", Action: "GET_EmployeeMaster");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Emplopyee_Master(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
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