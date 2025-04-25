using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class DM_ReferenceMappings : System.Web.UI.Page
    {
        string IndicatinID = null;
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    IndicatinID = Request.QueryString["IndicationID"].ToString();
                    bindVisit();
                    BINDMODULE();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bindVisit()
        {
            try
            {                
                 DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_VISIT_MASTER", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: IndicatinID);
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     grdVisit.DataSource = ds.Tables[0];
                     grdVisit.DataBind();                     
                 }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BINDMODULE()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "BINDMODULE", PROJECTID: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_Module.DataSource = ds.Tables[0];
                    grd_Module.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Module_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string ID = dr["ModuleID"].ToString();

                    GridView grd_Field = (GridView)e.Row.FindControl("grd_Field");
                    DataSet ds = dal.DM_ADD_UPDATE(ACTION: "BIDNFIELDSAGAINSMODULE", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: ID);
                    grd_Field.DataSource = ds.Tables[0];
                    grd_Field.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}