using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using ExcelDataReader;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class DM_CodeMapping : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    DataSet AutoCodeLIB = dal.DB_CODE_SP(ACTION: "GET_AUTOCODELEB", MODULEID: Request.QueryString["ID"].ToString());

                    if (AutoCodeLIB.Tables[0].Rows[0]["AutoCodeLIB"].ToString() == "MedDRA")
                    {
                        Meddra.Visible = true;
                        GET_MEDDRADATA();

                        if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);
                            grdMeddraData.Columns[17].Visible = false;
                        }
                        else
                        {
                            grdMeddraData.Columns[17].Visible = true;
                        }
                    }
                    else if (AutoCodeLIB.Tables[0].Rows[0]["AutoCodeLIB"].ToString() == "WHODD")
                    {
                        WHODData.Visible = true;
                        GET_WHODDDATA();

                        if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv_grdWhoddData();", true);
                            grdWhoddData.Columns[17].Visible = false;
                        }
                        else
                        {
                            grdWhoddData.Columns[17].Visible = true;
                        }
                    }

                    GET_WHODD_MEDDRA_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MEDDRADATA()
        {
            try
            {
                DataSet ds = dal.DB_CODE_SP(ACTION: "GET_MEDDRA_MAPPING_RECOARDS", MODULEID: Request.QueryString["ID"].ToString());

                if(ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdMeddraData.DataSource = ds;
                    grdMeddraData.DataBind();
                    divMeddraRecoard.Visible = true;
                    divWhoddRecoard.Visible = false;
                }
                else
                {
                    grdMeddraData.DataSource = null;
                    grdMeddraData.DataBind();
                    BIND_MEDDRA_DRP();
                    divMeddraRecoard.Visible = false;
                    divWhoddRecoard.Visible = false;
                }
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_WHODDDATA()
        {
            try
            {
                DataSet ds = dal.DB_CODE_SP(ACTION: "GET_WHODD_MAPPING_RECOARDS", MODULEID: Request.QueryString["ID"].ToString());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdWhoddData.DataSource = ds;
                    grdWhoddData.DataBind();
                    divMeddraRecoard.Visible = false;
                    divWhoddRecoard.Visible = true;
                }
                else
                {
                    grdWhoddData.DataSource = null;
                    grdWhoddData.DataBind();
                    BIND_WHODD_DRP();
                    divMeddraRecoard.Visible = false;
                    divWhoddRecoard.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void BIND_MEDDRA_DRP()
        {
            try
            {
                DataSet dt = dal.DB_CODE_SP(
                                ACTION: "BIND_DRP",
                                MODULEID: Request.QueryString["ID"].ToString()
                                );

                if (dt.Tables[0].Rows.Count > 0)
                {
                    BIND_MEDDRA(drpSystemOrganClass, dt.Tables[0]);
                    BIND_MEDDRA(drpSystemOrganClassCode, dt.Tables[0]);
                    BIND_MEDDRA(drpHighLevelGrpTerm, dt.Tables[0]);
                    BIND_MEDDRA(drpHighLevelGrpCode, dt.Tables[0]);
                    BIND_MEDDRA(drpHighlevelterm, dt.Tables[0]);
                    BIND_MEDDRA(drpHighleveltermCode, dt.Tables[0]);
                    BIND_MEDDRA(drpPererredTerm, dt.Tables[0]);
                    BIND_MEDDRA(drpPererredTermCode, dt.Tables[0]);
                    BIND_MEDDRA(drpLowestLevelTerm, dt.Tables[0]);
                    BIND_MEDDRA(drpLowestLevelTermCode, dt.Tables[0]);
                    BIND_MEDDRA(drMedDictionaryName, dt.Tables[0]);
                    BIND_MEDDRA(drMedDictionaryVer, dt.Tables[0]);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void BIND_MEDDRA(DropDownList ddl, DataTable dt)
        {
            try
            {
                ddl.DataSource = dt;
                ddl.DataValueField = "VARIABLENAME";
                ddl.DataTextField = "FIELDNAME";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        private void INSERT_MEDDRA()
        {
            try
            {
                dal.DB_CODE_SP(
                               ACTION: "INSERT_MEDDRA",
                               MODULEID: Request.QueryString["ID"].ToString(),
                               LLTCD: drpLowestLevelTermCode.SelectedValue,
                               LLTNM: drpLowestLevelTerm.SelectedValue,
                               PTCD: drpPererredTermCode.SelectedValue,
                               PTNM: drpPererredTerm.SelectedValue,
                               HLTCD: drpHighleveltermCode.SelectedValue,
                               HLTNM: drpHighlevelterm.SelectedValue,
                               HLGTCD: drpHighLevelGrpCode.SelectedValue,
                               HLGTNM: drpHighLevelGrpTerm.SelectedValue,
                               SOCCD: drpSystemOrganClassCode.SelectedValue,
                               SOCNM: drpSystemOrganClass.SelectedValue,
                               DICNM: drMedDictionaryName.SelectedValue,
                               DICNO: drMedDictionaryVer.SelectedValue
                           );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnMeddra_Click(object sender, EventArgs e)
        {
            try
            {
               
                List<string> selectedValues = new List<string>();
                List<string> duplicateValues = new List<string>();

                selectedValues.Add(drpSystemOrganClass.SelectedItem.Text);
                selectedValues.Add(drpSystemOrganClassCode.SelectedItem.Text);

                selectedValues.Add(drpHighLevelGrpTerm.SelectedItem.Text);
                selectedValues.Add(drpHighLevelGrpCode.SelectedItem.Text);
                
                selectedValues.Add(drpHighlevelterm.SelectedItem.Text);
                selectedValues.Add(drpHighleveltermCode.SelectedItem.Text);

                selectedValues.Add(drpPererredTerm.SelectedItem.Text);
                selectedValues.Add(drpPererredTermCode.SelectedItem.Text);

                selectedValues.Add(drpLowestLevelTerm.SelectedItem.Text);
                selectedValues.Add(drpLowestLevelTermCode.SelectedItem.Text);

                selectedValues.Add(drMedDictionaryName.SelectedItem.Text);
                selectedValues.Add(drMedDictionaryVer.SelectedItem.Text);

                var groupedValues = selectedValues.GroupBy(v => v);
                foreach (var group in groupedValues)
                {
                    if (group.Count() > 1)
                    {
                        duplicateValues.Add(group.Key);
                    }
                }
                if (duplicateValues.Count > 0)
                {
                    Response.Write("<script> alert('The fields: " + string.Join(", ", duplicateValues)+ " has alredy been selected for mapping.');</script>");
                }
                else
                {
                    INSERT_MEDDRA();
                    GET_MEDDRADATA();
                    Response.Redirect(Request.RawUrl);
                    Response.Write("<script> alert('Meddra codes mapped successfully.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        
        protected void btnMeddraCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BIND_WHODD_DRP()
        {
            try
            {
                DataSet dt = dal.DB_CODE_SP(
                                ACTION: "BIND_DRP",
                                MODULEID: Request.QueryString["ID"].ToString()
                                );

                if (dt.Tables[0].Rows.Count > 0)
                {
                    BIND_WHODD(drpATCLEVEL1, dt.Tables[0]);
                    BIND_WHODD(drpATCLEVEL1Code, dt.Tables[0]);
                    BIND_WHODD(drpATCLEVEL2, dt.Tables[0]);
                    BIND_WHODD(drpATCLEVEL2Code, dt.Tables[0]);
                    BIND_WHODD(drpATCLEVEL3, dt.Tables[0]);
                    BIND_WHODD(drpATCLEVEL3Code, dt.Tables[0]);
                    BIND_WHODD(drpATCLEVEL4, dt.Tables[0]);
                    BIND_WHODD(drpATCLEVEL4Code, dt.Tables[0]);
                    BIND_WHODD(drpATCLEVEL5, dt.Tables[0]);
                    BIND_WHODD(drpATCLEVEL5Code, dt.Tables[0]);
                    BIND_WHODD(drWhoDictionaryName, dt.Tables[0]);
                    BIND_WHODD(drWhoDictionaryVer, dt.Tables[0]);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void BIND_WHODD(DropDownList ddl, DataTable dt)
        {
            try
            {
                ddl.DataSource = dt;
                ddl.DataValueField = "VARIABLENAME";
                ddl.DataTextField = "FIELDNAME";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void btnWHODData_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> selectedValues = new List<string>();
                List<string> duplicateValues = new List<string>();

                selectedValues.Add(drpATCLEVEL1.SelectedItem.Text);
                selectedValues.Add(drpATCLEVEL1Code.SelectedItem.Text);

                selectedValues.Add(drpATCLEVEL2.SelectedItem.Text);
                selectedValues.Add(drpATCLEVEL2Code.SelectedItem.Text);

                selectedValues.Add(drpATCLEVEL3.SelectedItem.Text);
                selectedValues.Add(drpATCLEVEL3Code.SelectedItem.Text);

                selectedValues.Add(drpATCLEVEL4.SelectedItem.Text);
                selectedValues.Add(drpATCLEVEL4Code.SelectedItem.Text);

                selectedValues.Add(drpATCLEVEL5.SelectedItem.Text);
                selectedValues.Add(drpATCLEVEL5Code.SelectedItem.Text);

                selectedValues.Add(drWhoDictionaryName.SelectedItem.Text);
                selectedValues.Add(drWhoDictionaryVer.SelectedItem.Text);

                var groupedValues = selectedValues.GroupBy(v => v);
                foreach (var group in groupedValues)
                {
                    if (group.Count() > 1)
                    {
                        duplicateValues.Add(group.Key);
                    }
                }
                if (duplicateValues.Count > 0)
                {
                    Response.Write("<script> alert('The fields: " + string.Join(", ", duplicateValues) + " has alredy been selected for mapping.');</script>");
                }
                else
                {
                    INSERT_WHODD();
                    GET_WHODDDATA();
                    Response.Redirect(Request.RawUrl);
                    Response.Write("<script> alert('WHODD cades mapped successfully.');</script>");
                }  
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void INSERT_WHODD()
        {
            try
            {
                dal.DB_CODE_SP(
                               ACTION: "INSERT_WHODD",
                               MODULEID: Request.QueryString["ID"].ToString(),
                               CMATC1C: drpATCLEVEL1.SelectedValue,
                               CMATC1CD: drpATCLEVEL1Code.SelectedValue,
                               CMATC2C: drpATCLEVEL2.SelectedValue,
                               CMATC2CD: drpATCLEVEL2Code.SelectedValue,
                               CMATC3C: drpATCLEVEL3.SelectedValue,
                               CMATC3CD: drpATCLEVEL3Code.SelectedValue,
                               CMATC4C: drpATCLEVEL4.SelectedValue,
                               CMATC4CD: drpATCLEVEL4Code.SelectedValue,
                               CMATC5C: drpATCLEVEL5.SelectedValue,
                               CMATC5CD: drpATCLEVEL5Code.SelectedValue,
                               DICNM: drWhoDictionaryName.SelectedValue,
                               DICNO: drWhoDictionaryVer.SelectedValue
                           );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnWHODDataCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GET_WHODD_MEDDRA_DATA()
        {
            DataSet dt = dal.DB_CODE_SP(
                   ACTION: "GET_WHODD_MEDDRA_DATA",
                   MODULEID: Request.QueryString["ID"].ToString()
                    );

            if (dt.Tables[0].Rows.Count > 0)
            {
                drpSystemOrganClass.SelectedValue = dt.Tables[0].Rows[0]["SOCNM"].ToString();
                drpSystemOrganClassCode.SelectedValue = dt.Tables[0].Rows[0]["SOCCD"].ToString();
                drpHighLevelGrpTerm.SelectedValue = dt.Tables[0].Rows[0]["HLGTNM"].ToString();
                drpHighLevelGrpCode.SelectedValue = dt.Tables[0].Rows[0]["HLGTCD"].ToString();
                drpHighlevelterm.SelectedValue = dt.Tables[0].Rows[0]["HLTNM"].ToString();
                drpHighleveltermCode.SelectedValue = dt.Tables[0].Rows[0]["HLTCD"].ToString();
                drpPererredTerm.SelectedValue = dt.Tables[0].Rows[0]["PTNM"].ToString();
                drpPererredTermCode.SelectedValue = dt.Tables[0].Rows[0]["PTCD"].ToString();
                drpLowestLevelTerm.SelectedValue = dt.Tables[0].Rows[0]["LLTNM"].ToString();
                drpLowestLevelTermCode.SelectedValue = dt.Tables[0].Rows[0]["LLTCD"].ToString();
                drpATCLEVEL1.SelectedValue = dt.Tables[0].Rows[0]["CMATC1C"].ToString();
                drpATCLEVEL1Code.SelectedValue = dt.Tables[0].Rows[0]["CMATC1CD"].ToString();
                drpATCLEVEL2.SelectedValue = dt.Tables[0].Rows[0]["CMATC2C"].ToString();
                drpATCLEVEL2Code.SelectedValue = dt.Tables[0].Rows[0]["CMATC2CD"].ToString();
                drpATCLEVEL3.SelectedValue = dt.Tables[0].Rows[0]["CMATC3C"].ToString();
                drpATCLEVEL3Code.SelectedValue = dt.Tables[0].Rows[0]["CMATC3CD"].ToString();
                drpATCLEVEL4.SelectedValue = dt.Tables[0].Rows[0]["CMATC4C"].ToString();
                drpATCLEVEL4Code.SelectedValue = dt.Tables[0].Rows[0]["CMATC4CD"].ToString();
                drpATCLEVEL5.SelectedValue = dt.Tables[0].Rows[0]["CMATC5C"].ToString();
                drpATCLEVEL5Code.SelectedValue = dt.Tables[0].Rows[0]["CMATC5CD"].ToString();
                drMedDictionaryName.SelectedValue = dt.Tables[0].Rows[0]["DICNM"].ToString();
                drMedDictionaryVer.SelectedValue = dt.Tables[0].Rows[0]["DICNO"].ToString();
                drWhoDictionaryName.SelectedValue = dt.Tables[0].Rows[0]["DICNM"].ToString();
                drWhoDictionaryVer.SelectedValue = dt.Tables[0].Rows[0]["DICNO"].ToString();

                if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                {
                    if (Meddra.Visible == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);
                    }
                    else if (WHODData.Visible == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv_grdWhoddData();", true);
                    }
                }
            }
            else
            {

            }
        }

        protected void grdWhoddData_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdMeddraData_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdMeddraData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                if(e.CommandName == "EditMeddra")
                {
                    EDIT_MEDDRA();
                    btnMeddraUpdate.Visible = true;
                    btnMeddra.Visible = false;

                }
                else if(e.CommandName == "DeleteMeddra")
                {
                    DELETE_MEDDRA(id);
                    GET_MEDDRADATA();
                }

            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT_MEDDRA()
        {
            try
            {
                BIND_MEDDRA_DRP();
                GET_WHODD_MEDDRA_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_MEDDRA(string id)
        {
            try
            {
                DataSet ds = dal.DB_CODE_SP(ACTION: "DELETE_MEDDRA_RECOARDS", MODULEID: Request.QueryString["ID"].ToString(),ID:id);

                Response.Write("<script> alert('Meddra Mapping Delete successfully.');</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdWhoddData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                if (e.CommandName == "EditWhodd")
                {
                    EDIT_WHODD();
                    btnWODData.Visible = true;
                    btnWHODData.Visible = false;
                }
                else if (e.CommandName == "DeleteWhodd")
                {
                    DELETE_WHODD(id);
                    GET_WHODDDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT_WHODD()
        {
            try
            {
                BIND_WHODD_DRP();
                GET_WHODD_MEDDRA_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_WHODD(string id)
        {
            try
            {
                DataSet ds = dal.DB_CODE_SP(ACTION: "DELETE_WHODD_RECOARDS", MODULEID: Request.QueryString["ID"].ToString(), ID: id);

                Response.Write("<script> alert('WHODD Mapping Delete successfully.');</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        
    }
}