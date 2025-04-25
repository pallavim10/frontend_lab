using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DM_DATA : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lblModulename.Text = Request.QueryString["MODULENAME"].ToString();

                GetRecords();
            }
        }
        private void GetRecords()
        {
            try
            {
                DataSet dsData = dal.DM_MODULE_DATA_VIEW_SP(
                    PVID: Request.QueryString["PVID"].ToString(),
                    TABLENAME: Request.QueryString["TABLENAME"].ToString(),
                    VISITNUM: Request.QueryString["VISITID"].ToString(),
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    RECID: Request.QueryString["RECID"].ToString()
                    );

                DataSet ds = new DataSet();
                if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = GenerateTransposedTable(dsData.Tables[0]);
                    ds.Tables.Add(dt);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_Data.DataSource = ds;
                        grd_Data.DataBind();
                    }
                    else
                    {
                        grd_Data.DataSource = null;
                        grd_Data.DataBind();
                    }
                }
                else
                {
                    grd_Data.DataSource = null;
                    grd_Data.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
               
            }
        }
        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("Field Name");
            outputTable.Columns.Add("Data");

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                foreach (DataColumn dc in inputTable.Columns)
                {
                    DataRow drNew = outputTable.NewRow();
                    drNew["Field Name"] = dc.ColumnName.ToString();
                    drNew["Data"] = REMOVEHTML(inputTable.Rows[i][dc.ColumnName].ToString());
                    outputTable.Rows.Add(drNew);
                }
            }

            return outputTable;
        }

        protected static string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");

            return str;
        }
    }
}