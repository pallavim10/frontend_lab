using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_ALLDOCS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    //Get_Files();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_OPTIONS(DropDownList ddl, HiddenField hf)
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_eTMF.eTMF_REPORT_SP(ACTION: "GET_SEARCH_OPTIONS", CritCode: ddl.SelectedValue);

                string Values = "";
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Values += "" + ds.Tables[0].Rows[i]["VALUE"].ToString() + ",";
                    }
                }
                hf.Value = Values.TrimEnd(',');

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField1, hfValue1);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField2, hfValue2);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField3, hfValue3);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField4, hfValue4);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField5, hfValue5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Get_Files();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnAllDocs_Click(object sender, EventArgs e)
        {
            try
            {
                Get_ALL_Files();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void Get_ALL_Files()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_REPORT_SP(ACTION: "GET_SEARCH_DOCS");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvFiles.DataSource = ds;
                    gvFiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Files()
        {
            try
            {
                string
                           FIELDNAME1 = drpLISTField1.SelectedValue,
                FIELDNAME2 = drpLISTField2.SelectedValue,
                FIELDNAME3 = drpLISTField3.SelectedValue,
                FIELDNAME4 = drpLISTField4.SelectedValue,
                FIELDNAME5 = drpLISTField5.SelectedValue,
             Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
             Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
             Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null, AndOr5 = null,
             CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;
                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpLISTAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "IS NULL")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "[_]%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                }
                else if (Condition1 == "![_]%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                }
                else if (Condition1 == "%_%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                }
                else if (Condition1 == "!%_%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                }
                else
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                }

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    Condition2 = drpLISTCondition2.SelectedValue;
                    Value2 = txtLISTValue2.Text;
                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpLISTAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "IS NULL")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " ";
                    }
                    else if (Condition2 == "[_]%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "![_]%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "%_%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "!%_%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
                    }

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        Condition3 = drpLISTCondition3.SelectedValue;
                        Value3 = txtLISTValue3.Text;

                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpLISTAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "IS NULL")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " ";
                        }
                        else if (Condition3 == "[_]%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "![_]%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "%_%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "!%_%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
                        }

                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            Condition4 = drpLISTCondition4.SelectedValue;
                            Value4 = txtLISTValue4.Text;

                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpLISTAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "IS NULL")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " ";
                            }
                            else if (Condition4 == "[_]%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "![_]%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "%_%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "!%_%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                            }

                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                Condition5 = drpLISTCondition5.SelectedValue;
                                Value5 = txtLISTValue5.Text;

                                if (Condition5 == "IS NULL")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '[" + Value5 + "]%' " + AndOr5 + " ";
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '[" + Value5 + "]%' " + AndOr5 + " ";
                                }
                                else if (Condition5 == "%_%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '%" + Value5 + "%' " + AndOr5 + " ";
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '%" + Value5 + "%' " + AndOr5 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " '" + Value5 + "' " + AndOr5 + " ";
                                }
                            }
                        }
                    }
                }

                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5;

                DataSet ds = dal_eTMF.eTMF_REPORT_SP(ACTION: "GET_SEARCH_DOCS",
                CritCode: CritCodeQUERY
                );

                if (ds.Tables.Count > 0)
                {
                    gvFiles.DataSource = ds;
                    gvFiles.DataBind();
                }
                else
                {
                    gvFiles.DataSource = null;
                    gvFiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "Download_File", ID: e.CommandArgument.ToString());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //File to be downloaded.
                    string fileName = ds.Tables[0].Rows[0]["SysFileName"].ToString();

                    //Set the New File name.
                    string newFileName = ds.Tables[0].Rows[0]["UploadFileName"].ToString();


                    //Path of the File to be downloaded.
                    string filePath = Server.MapPath(string.Format("~/eTMF_Docs/{0}", fileName));

                    //Setting the Content Type, Header and the new File name.
                    Response.ContentType = ds.Tables[0].Rows[0]["FileType"].ToString();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + newFileName);

                    // Append cookie
                    HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                    cookie.Value = "Flag";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie);
                    // end

                    //Writing the File to Response Stream.
                    Response.WriteFile(filePath);
                    Response.Flush();
                    Response.End();
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

        protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();
                    string FileType = drv["FileType"].ToString();
                    Label lbtnFileType = (Label)e.Row.FindControl("lbtnFileType");
                    HtmlControl ICON = (HtmlControl)e.Row.FindControl("ICONCLASS");

                    if (FileType == "application/vnd.ms-excel" || FileType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        ICON.Attributes.Add("class", "fa fa-file-excel");
                        lbtnFileType.ToolTip = "Excel File";
                    }
                    else if (FileType == "application/msword" || FileType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    {
                        ICON.Attributes.Add("class", "fa fa-file-word");
                        lbtnFileType.ToolTip = "Word File";
                    }
                    else if (FileType == "application/pdf")
                    {
                        ICON.Attributes.Add("class", "fa fa-file-pdf");
                        lbtnFileType.ToolTip = "PDF File";
                    }
                    else if (FileType.Contains("image/"))
                    {
                        ICON.Attributes.Add("class", "fa fa-file-image");
                        lbtnFileType.ToolTip = "Image File";
                    }
                    else if (FileType == "application/vnd.ms-powerpoint" || FileType == "application/vnd.openxmlformats-officedocument.presentationml.presentation")
                    {
                        ICON.Attributes.Add("class", "fa fa-file-powerpoint");
                        lbtnFileType.ToolTip = "PPT File";
                    }
                    else
                    {
                        ICON.Attributes.Add("class", "fa fa-file-text");
                        lbtnFileType.ToolTip = "TEXT File";
                    }

                    HtmlControl iconhistory = (HtmlControl)e.Row.FindControl("iconhistory");

                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "GET_VERSION_HISTORY_COUNT", ID: ID);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["COUNTS"].ToString() != "0")
                        {
                            iconhistory.Attributes.Add("style", "color: red;");
                        }
                    }

                    HtmlControl iconQC = (HtmlControl)e.Row.FindControl("iconQC");

                    if (drv["QC"].ToString() == "True")
                    {
                        iconQC.Attributes.Add("class", "fa fa-check");
                        iconQC.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconQC.Attributes.Add("class", "fa fa-times");
                        iconQC.Attributes.Add("style", "color: red;");
                    }

                    string UnblindDoc = drv["Unblind"].ToString();

                    Label lbl_UploadFileName = (Label)e.Row.FindControl("lbl_UploadFileName");
                    LinkButton lbtn_UploadFileName = (LinkButton)e.Row.FindControl("lbtn_UploadFileName");
                    LinkButton lbtnDownloadDoc = (LinkButton)e.Row.FindControl("lbtnDownloadDoc");

                    if (UnblindDoc == "Unblinded")
                    {
                        if (Session["Unblind"].ToString() == "Unblinded")
                        {
                            lbtnDownloadDoc.Visible = true;
                            lbl_UploadFileName.Visible = false;
                            lbtn_UploadFileName.Visible = true;
                        }
                        else
                        {
                            lbtnDownloadDoc.Visible = false;
                            lbtn_UploadFileName.Visible = false;
                            lbl_UploadFileName.Visible = true;
                        }
                    }
                    else
                    {
                        lbl_UploadFileName.Visible = false;
                        lbtn_UploadFileName.Visible = true;
                        lbtnDownloadDoc.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                string
                           FIELDNAME1 = drpLISTField1.SelectedValue,
                FIELDNAME2 = drpLISTField2.SelectedValue,
                FIELDNAME3 = drpLISTField3.SelectedValue,
                FIELDNAME4 = drpLISTField4.SelectedValue,
                FIELDNAME5 = drpLISTField5.SelectedValue,
             Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
             Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
             Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null, AndOr5 = null,
             CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;
                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpLISTAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "IS NULL")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "[_]%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                }
                else if (Condition1 == "![_]%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                }
                else if (Condition1 == "%_%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                }
                else if (Condition1 == "!%_%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                }
                else
                {
                    if (drpLISTField1.SelectedValue == "0")
                    {
                        CritCodeQUERY1 = "";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                    }
                }

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    Condition2 = drpLISTCondition2.SelectedValue;
                    Value2 = txtLISTValue2.Text;
                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpLISTAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "IS NULL")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " ";
                    }
                    else if (Condition2 == "[_]%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "![_]%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "%_%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "!%_%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
                    }

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        Condition3 = drpLISTCondition3.SelectedValue;
                        Value3 = txtLISTValue3.Text;

                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpLISTAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "IS NULL")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " ";
                        }
                        else if (Condition3 == "[_]%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "![_]%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "%_%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "!%_%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
                        }

                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            Condition4 = drpLISTCondition4.SelectedValue;
                            Value4 = txtLISTValue4.Text;

                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpLISTAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "IS NULL")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " ";
                            }
                            else if (Condition4 == "[_]%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "![_]%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "%_%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "!%_%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                            }

                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                Condition5 = drpLISTCondition5.SelectedValue;
                                Value5 = txtLISTValue5.Text;

                                if (Condition5 == "IS NULL")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '[" + Value5 + "]%' " + AndOr5 + " ";
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '[" + Value5 + "]%' " + AndOr5 + " ";
                                }
                                else if (Condition5 == "%_%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '%" + Value5 + "%' " + AndOr5 + " ";
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '%" + Value5 + "%' " + AndOr5 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " '" + Value5 + "' " + AndOr5 + " ";
                                }
                            }
                        }
                    }
                }

                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5;

                DataSet ds = dal_eTMF.eTMF_REPORT_SP(ACTION: "GET_SEARCH_DOCS_Export",
                CritCode: CritCodeQUERY
                );

                ds.Tables[0].TableName = "Search Docs";
                Multiple_Export_Excel.ToExcel(ds, "Search Docs" + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                string
                           FIELDNAME1 = drpLISTField1.SelectedValue,
                FIELDNAME2 = drpLISTField2.SelectedValue,
                FIELDNAME3 = drpLISTField3.SelectedValue,
                FIELDNAME4 = drpLISTField4.SelectedValue,
                FIELDNAME5 = drpLISTField5.SelectedValue,
             Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
             Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
             Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null, AndOr5 = null,
             CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;
                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpLISTAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "IS NULL")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "[_]%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                }
                else if (Condition1 == "![_]%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                }
                else if (Condition1 == "%_%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                }
                else if (Condition1 == "!%_%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                }
                else
                {
                    if (drpLISTField1.SelectedValue == "0")
                    {
                        CritCodeQUERY1 = "";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                    }
                }

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    Condition2 = drpLISTCondition2.SelectedValue;
                    Value2 = txtLISTValue2.Text;
                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpLISTAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "IS NULL")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " ";
                    }
                    else if (Condition2 == "[_]%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "![_]%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "%_%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "!%_%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
                    }

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        Condition3 = drpLISTCondition3.SelectedValue;
                        Value3 = txtLISTValue3.Text;

                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpLISTAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "IS NULL")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " ";
                        }
                        else if (Condition3 == "[_]%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "![_]%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "%_%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "!%_%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
                        }

                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            Condition4 = drpLISTCondition4.SelectedValue;
                            Value4 = txtLISTValue4.Text;

                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpLISTAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "IS NULL")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " ";
                            }
                            else if (Condition4 == "[_]%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "![_]%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "%_%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "!%_%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                            }

                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                Condition5 = drpLISTCondition5.SelectedValue;
                                Value5 = txtLISTValue5.Text;

                                if (Condition5 == "IS NULL")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '[" + Value5 + "]%' " + AndOr5 + " ";
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '[" + Value5 + "]%' " + AndOr5 + " ";
                                }
                                else if (Condition5 == "%_%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '%" + Value5 + "%' " + AndOr5 + " ";
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '%" + Value5 + "%' " + AndOr5 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " '" + Value5 + "' " + AndOr5 + " ";
                                }
                            }
                        }
                    }
                }

                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5;

                DataSet ds = dal_eTMF.eTMF_REPORT_SP(ACTION: "GET_SEARCH_DOCS_Export",
                CritCode: CritCodeQUERY
                );

                ds.Tables[0].TableName = lblHeader.Text;

                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}