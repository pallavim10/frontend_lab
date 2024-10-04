using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class UMT_PROJECT : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_DATA();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void GET_DATA()
        {
            try
            {
                DataSet ds = dal_UMT.PROJECT_SP(
                          ACTION: "GET_DATA"
                          );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtProjectName.Text = ds.Tables[0].Rows[0]["PROJNAME"].ToString();
                    txtSponsorName.Text = ds.Tables[0].Rows[0]["SPONSOR_NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void UPDATE_DATA()
        {
            try
            {
                DataSet ds = dal_UMT.PROJECT_SP(
                          ACTION: "UPDATE_DATA",
                          PROJECT_NAME: txtProjectName.Text,
                          SPONSOR_NAME: txtSponsorName.Text
                          );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtProjectName.Text = ds.Tables[0].Rows[0]["PROJNAME"].ToString();
                    txtSponsorName.Text = ds.Tables[0].Rows[0]["SPONSOR_NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_DATA();

                string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Details updated Successfully.',
                     icon: 'success',
                     button: 'OK'
                     }).then((value) => {
                        window.location.href = window.location.href; 
                     });";

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Project Details Report";

                DataSet ds = dal_UMT.PROJECT_SP(
                   ACTION: "EXPORT_DATA"
                   );

                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}