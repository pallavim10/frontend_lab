using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PPT
{
    public partial class frmPROJDETAILS : System.Web.UI.Page
    {

        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!this.IsPostBack)
                {
                    fill_Product();
                    // GetRecords();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        public void GetRecords()
        {
            try
            {
                dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                ds = dal.GetSetPROJECTDETAILS(Action: "GET_DATA", PRODUCTID: drpProduct.SelectedItem.Value);
                PROJDETAILS.DataSource = ds;
                PROJDETAILS.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void InsertUpdatePROJECTDETAILS(int rownum)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                dal.GetSetPROJECTDETAILS(
                Action: "INSERT",
                Project_ID: Convert.ToInt16(((TextBox)PROJDETAILS.Rows[rownum].FindControl("Project_ID")).Text),
                PRODUCTID: drpProduct.SelectedItem.Value,
                PROJNAME: ((TextBox)PROJDETAILS.Rows[rownum].FindControl("PROJNAME")).Text,
                SPONSOR: ((TextBox)PROJDETAILS.Rows[rownum].FindControl("SPONSOR")).Text,
                PROJSTDAT: ((TextBox)PROJDETAILS.Rows[rownum].FindControl("PROJSTDAT")).Text,
                PROJDUR: ((TextBox)PROJDETAILS.Rows[rownum].FindControl("PROJDUR")).Text,
                ENTEREDBY: Session["User_ID"].ToString()
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                //int rownum;
                //for (rownum = 0; rownum < PROJDETAILS.Rows.Count; rownum++)
                //{
                //    if (((TextBox)PROJDETAILS.Rows[rownum].FindControl("Project_ID")).Text == "")
                //    {
                //        lblErrorMsg.Text = "Please enter Project Id.";
                //    }
                //    else if (((TextBox)PROJDETAILS.Rows[rownum].FindControl("PROJNAME")).Text == "")
                //    {
                //        lblErrorMsg.Text = "Please enter Project Name.";
                //    }
                //    else
                //    {
                //        InsertUpdatePROJECTDETAILS(rownum);
                //    }
                //   }
                DAL dal;
                dal = new DAL();
                dal.GetSetPROJECTDETAILS(
                Action: "INSERT",
                Project_ID: Convert.ToInt32(txt_Project_ID.Text),
                PRODUCTID: drpProduct.SelectedItem.Value,
                PROJNAME: txtProjName.Text,
                SPONSOR: txtSponser.Text,
                PROJSTDAT: txtStartDate.Text,
                PROJDUR: txtDuration.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                THERAREA: txtTHERAREA.Text,
                INDC: txtINDC.Text,
                PHASE: txtPHASE.Text,
                ISACTIVE: chkISACTIVE.Checked,
                Re_Screen_WP: Convert.ToInt32(txt_Re_Screen_WP.Text),
                Rand_WP: Convert.ToInt32(txtRand_WP.Text),
                NoReScreen: Convert.ToInt32(txtNoReScreen.Text),
                Re_Screen_WP_End: Convert.ToInt32(txtRe_Screen_WP_End.Text),
                Rand_WP_End: Convert.ToInt32(txtRand_WP_End.Text),
                Rand_No_Site_Specific: chkRand_No_Site_Specific.Checked,
                MMA_Approval_Req: chkMMA_Approval_Req.Checked,
                ProjectStrataYN: chkProjectStrataYN.Checked,
                GenderStrataYN: chkGenderStrataYN.Checked
                );
                Response.Write("<script> alert('Record Updated successfully.');window.location='frmPROJDETAILS.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Product()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Product"
                );
                drpProduct.DataSource = ds.Tables[0];
                drpProduct.DataValueField = "PRODUCTID";
                drpProduct.DataTextField = "PRODUCTNAM";
                drpProduct.DataBind();
                drpProduct.Items.Insert(0, new ListItem("--Select Product--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void drpProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRecords();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void PROJDETAILS_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                string ProjectId = dr["Project_ID"].ToString();
                if (ProjectId != "")
                {
                    e.Row.Cells[0].Enabled = false;
                }
            }
        }


        protected void bntAdd_Click(object sender, EventArgs e)
        {
            try
            {
                divForm.Visible = true;
                txt_Project_ID.Enabled = true;
                txt_Project_ID.Text = "";
                txtProjName.Text = "";
                txtSponser.Text = "";
                txtStartDate.Text = "";
                txtDuration.Text = "";
                txtTHERAREA.Text = "";
                txtINDC.Text = "";
                txtPHASE.Text = "";
                chkISACTIVE.Checked = false;
                txt_Re_Screen_WP.Text = "";
                txtRand_WP.Text = "";
                txtNoReScreen.Text = "";
                txtRe_Screen_WP_End.Text = "";
                txtRand_WP_End.Text = "";
                chkRand_No_Site_Specific.Checked = false;
                chkMMA_Approval_Req.Checked = false;
                chkProjectStrataYN.Checked = false;
                chkGenderStrataYN.Checked = false;

                ////Table Structure
                //DataRow drCurrentRow = null;
                //DataTable dtCurrentTable;
                //int rowIndex = 0;
                //int i;
                //dtCurrentTable = new DataTable();
                //dtCurrentTable.Columns.Add(new DataColumn("Project_ID", typeof(string)));
                //dtCurrentTable.Columns.Add(new DataColumn("PROJNAME", typeof(string)));
                //dtCurrentTable.Columns.Add(new DataColumn("SPONSOR", typeof(string)));
                //dtCurrentTable.Columns.Add(new DataColumn("PROJSTDAT", typeof(string)));
                //dtCurrentTable.Columns.Add(new DataColumn("PROJDUR", typeof(string)));

                //if (PROJDETAILS.Rows.Count > 0)
                //{
                //    for (i = 0; i < PROJDETAILS.Rows.Count; i++)
                //    {
                //        drCurrentRow = dtCurrentTable.NewRow();

                //        drCurrentRow["Project_ID"] = ((TextBox)PROJDETAILS.Rows[rowIndex].FindControl("Project_ID")).Text;
                //        drCurrentRow["PROJNAME"] = ((TextBox)PROJDETAILS.Rows[rowIndex].FindControl("PROJNAME")).Text;
                //        drCurrentRow["SPONSOR"] = ((TextBox)PROJDETAILS.Rows[rowIndex].FindControl("SPONSOR")).Text;
                //        drCurrentRow["PROJSTDAT"] = ((TextBox)PROJDETAILS.Rows[rowIndex].FindControl("PROJSTDAT")).Text;
                //        drCurrentRow["PROJDUR"] = ((TextBox)PROJDETAILS.Rows[rowIndex].FindControl("PROJDUR")).Text;
                //        dtCurrentTable.Rows.Add(drCurrentRow);
                //        rowIndex++;
                //    }

                //    //Add Empty Row
                //    drCurrentRow = dtCurrentTable.NewRow();
                //    drCurrentRow = dtCurrentTable.NewRow();

                //    drCurrentRow["Project_ID"] = "";
                //    drCurrentRow["PROJNAME"] = "";
                //    drCurrentRow["SPONSOR"] = "";
                //    drCurrentRow["PROJSTDAT"] = "";
                //    drCurrentRow["PROJDUR"] = "";
                //    dtCurrentTable.Rows.Add(drCurrentRow);
                //}
                //PROJDETAILS.DataSource = dtCurrentTable;
                //PROJDETAILS.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        protected void PROJDETAILS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    divForm.Visible = true;
                    int Project_ID = Convert.ToInt32(e.CommandArgument);
                    DAL dal;
                    dal = new DAL();
                    DataSet DS = dal.GetSetPROJECTDETAILS
                    (
                    Action: "Get_ProjDetails",
                    Project_ID: Project_ID,
                    PRODUCTID: drpProduct.SelectedItem.Value
                    );
                    if (DS.Tables.Count > 0)
                    {
                        txt_Project_ID.Enabled = false;
                        txt_Project_ID.Text = DS.Tables[0].Rows[0]["Project_ID"].ToString();
                        txtProjName.Text = DS.Tables[0].Rows[0]["PROJNAME"].ToString();
                        txtSponser.Text = DS.Tables[0].Rows[0]["SPONSOR"].ToString();
                        txtStartDate.Text = DS.Tables[0].Rows[0]["PROJSTDAT"].ToString();
                        txtDuration.Text = DS.Tables[0].Rows[0]["PROJDUR"].ToString();
                        txtTHERAREA.Text = DS.Tables[0].Rows[0]["THERAREA"].ToString();
                        txtINDC.Text = DS.Tables[0].Rows[0]["INDC"].ToString();
                        txtPHASE.Text = DS.Tables[0].Rows[0]["PHASE"].ToString();
                        chkISACTIVE.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0]["ISACTIVE"]);
                        txt_Re_Screen_WP.Text = DS.Tables[0].Rows[0]["Re_Screen_WP"].ToString();
                        txtRand_WP.Text = DS.Tables[0].Rows[0]["Rand_WP"].ToString();
                        txtNoReScreen.Text = DS.Tables[0].Rows[0]["NoReScreen"].ToString();
                        txtRe_Screen_WP_End.Text = DS.Tables[0].Rows[0]["Re_Screen_WP_End"].ToString();
                        txtRand_WP_End.Text = DS.Tables[0].Rows[0]["Rand_WP_End"].ToString();
                        chkRand_No_Site_Specific.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0]["Rand_No_Site_Specific"].ToString());
                        chkMMA_Approval_Req.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0]["MMA_Approval_Req"].ToString());
                        chkProjectStrataYN.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0]["ProjectStrataYN"].ToString());
                        chkGenderStrataYN.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0]["GenderStrataYN"].ToString());
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