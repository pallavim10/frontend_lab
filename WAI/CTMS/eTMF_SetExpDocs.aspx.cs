using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using Ionic.Zip;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_SetExpDocs : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();

        DataSet dsUsers = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["prevURL"] = Request.Url.PathAndQuery.ToString();

                        bind_Folder();

                        if (Session["SET_ZoneID"] != null && Session["SET_SectionID"] != null)
                        {
                            ddlZone.SelectedValue = Session["SET_ZoneID"].ToString();

                            BIND_SECTIONS();
                            ddlSections.SelectedValue = Session["SET_SectionID"].ToString();

                            ddlSections_SelectedIndexChanged(sender, e);

                            Session.Remove("SET_ZoneID");
                            Session.Remove("SET_SectionID");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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

        private void bind_Folder()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_Folder", Project_ID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlZone.DataSource = ds.Tables[0];
                    ddlZone.DataValueField = "ID";
                    ddlZone.DataTextField = "Folder";
                    ddlZone.DataBind();
                    ddlZone.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlZone.DataSource = null;
                    ddlZone.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_SECTIONS();

                BIND_ARTIFACT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_SECTIONS()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_SubFolder",
                    ID: ddlZone.SelectedValue,
                    Project_ID: Session["PROJECTID"].ToString()
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSections.DataSource = ds.Tables[0];
                    ddlSections.DataValueField = "SubFolder_ID";
                    ddlSections.DataTextField = "Sub_Folder_Name";
                    ddlSections.DataBind();
                    ddlSections.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlSections.DataSource = null;
                    ddlSections.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dsUsers = new DataSet();
                dsUsers = dal_eTMF.eTMF_SET_SP(ACTION: "GetUsers");

                BIND_ARTIFACT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_ARTIFACT()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_ProjectArtifacts",
                SubFolder_ID: ddlSections.SelectedValue,
                Folder_ID: ddlZone.SelectedValue,
                Project_ID: Session["PROJECTID"].ToString(),
                User: Session["User_ID"].ToString()
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvArtifact.DataSource = ds.Tables[0];
                    gvArtifact.DataBind();
                }
                else
                {
                    gvArtifact.DataSource = null;
                    gvArtifact.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvFolder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Folder_ID = drv["Folder_ID"].ToString();

                    GridView gvSubFolder = e.Row.FindControl("gvSubFolder") as GridView;
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_SubFolder", ID: Folder_ID, Project_ID: Session["PROJECTID"].ToString());
                    gvSubFolder.DataSource = ds.Tables[0];
                    gvSubFolder.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvSubFolder.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvSubFolder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Folder_ID = drv["Folder_ID"].ToString();
                    string SubFolder_ID = drv["SubFolder_ID"].ToString();

                    GridView gvArtifact = e.Row.FindControl("gvArtifact") as GridView;
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_ProjectArtifacts", SubFolder_ID: SubFolder_ID, Folder_ID: Folder_ID, Project_ID: Session["PROJECTID"].ToString(), User: Session["User_ID"].ToString());
                    gvArtifact.DataSource = ds.Tables[0];
                    gvArtifact.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvArtifact.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void gvArtifact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();

                    GridView gvDocs = e.Row.FindControl("gvDocs") as GridView;
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_BD_Docs", ID: ID);
                    gvDocs.DataSource = ds.Tables[0];
                    gvDocs.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvDocs.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string REVIEW = drv["REVIEW"].ToString();

                    if (REVIEW == "True")
                    {
                        Label blockedRefNo = e.Row.FindControl("blockedRefNo") as Label;
                        TextBox txtRefNo = e.Row.FindControl("txtRefNo") as TextBox;
                        blockedRefNo.Visible = true;
                        txtRefNo.Visible = false;

                        Label blockedDocName = e.Row.FindControl("blockedDocName") as Label;
                        TextBox txtDocName = e.Row.FindControl("txtDocName") as TextBox;
                        blockedDocName.Visible = true;
                        txtDocName.Visible = false;

                        Label blockedAutoReplace = e.Row.FindControl("blockedAutoReplace") as Label;
                        DropDownList ddlAutoReplace = e.Row.FindControl("ddlAutoReplace") as DropDownList;
                        blockedAutoReplace.Visible = true;
                        ddlAutoReplace.Visible = false;

                        Label blockedVerType = e.Row.FindControl("blockedVerType") as Label;
                        DropDownList ddlVerType = e.Row.FindControl("ddlVerType") as DropDownList;
                        blockedVerType.Visible = true;
                        ddlVerType.Visible = false;

                        Label blockedVerDate = e.Row.FindControl("blockedVerDate") as Label;
                        LinkButton lbtnVerDateYES = e.Row.FindControl("lbtnVerDateYES") as LinkButton;
                        LinkButton lbtnVerDateNO = e.Row.FindControl("lbtnVerDateNO") as LinkButton;
                        blockedVerDate.Visible = true;
                        lbtnVerDateYES.Visible = false;
                        lbtnVerDateNO.Visible = false;

                        Label blockedVerSPEC = e.Row.FindControl("blockedVerSPEC") as Label;
                        LinkButton lbtnVerSPECYES = e.Row.FindControl("lbtnVerSPECYES") as LinkButton;
                        LinkButton lbtnVerSPECNO = e.Row.FindControl("lbtnVerSPECNO") as LinkButton;
                        blockedVerSPEC.Visible = true;
                        lbtnVerSPECYES.Visible = false;
                        lbtnVerSPECNO.Visible = false;

                        Label blockedUnblind = e.Row.FindControl("blockedUnblind") as Label;
                        DropDownList ddlUnblind = e.Row.FindControl("ddlUnblind") as DropDownList;
                        blockedUnblind.Visible = true;
                        ddlUnblind.Visible = false;

                        LinkButton lbtnOptions = e.Row.FindControl("lbtnOptions") as LinkButton;
                        lbtnOptions.Visible = false;

                        LinkButton lbtnDeleteDoc = e.Row.FindControl("lbtnDeleteDoc") as LinkButton;
                        lbtnDeleteDoc.Visible = false;
                    }
                    else
                    {
                        string AutoReplace = drv["AutoReplace"].ToString();
                        DropDownList ddlAutoReplace = e.Row.FindControl("ddlAutoReplace") as DropDownList;
                        if (AutoReplace != "" && AutoReplace != "0")
                        {
                            ddlAutoReplace.SelectedValue = AutoReplace;
                        }
                        else
                        {
                            ddlAutoReplace.SelectedValue = "0";
                        }

                        string VerTYPE = drv["VerTYPE"].ToString();
                        DropDownList ddlVerType = e.Row.FindControl("ddlVerType") as DropDownList;
                        if (VerTYPE != "" && VerTYPE != "0")
                        {
                            ddlVerType.SelectedValue = VerTYPE;
                        }
                        else
                        {
                            ddlVerType.SelectedValue = "0";
                        }

                        string VerDate = drv["VerDate"].ToString();
                        LinkButton lbtnVerDateYES = e.Row.FindControl("lbtnVerDateYES") as LinkButton;
                        LinkButton lbtnVerDateNO = e.Row.FindControl("lbtnVerDateNO") as LinkButton;
                        if (VerDate == "True")
                        {
                            lbtnVerDateYES.CssClass += " disp-none";
                            lbtnVerDateNO.CssClass = lbtnVerDateNO.CssClass.Replace("disp-none", "");
                        }
                        else
                        {
                            lbtnVerDateYES.CssClass = lbtnVerDateYES.CssClass.Replace("disp-none", "");
                            lbtnVerDateNO.CssClass += " disp-none";
                        }

                        string Unblind = drv["Unblind"].ToString();
                        DropDownList ddlUnblind = e.Row.FindControl("ddlUnblind") as DropDownList;
                        if (Unblind != "" && Unblind != "0")
                        {
                            ddlUnblind.SelectedValue = Unblind;
                        }
                        else
                        {
                            ddlUnblind.SelectedValue = "0";
                        }

                        string VerSPEC = drv["VerSPEC"].ToString();
                        LinkButton lbtnVerSPECYES = e.Row.FindControl("lbtnVerSPECYES") as LinkButton;
                        LinkButton lbtnVerSPECNO = e.Row.FindControl("lbtnVerSPECNO") as LinkButton;
                        if (VerSPEC != "" && VerSPEC == "True")
                        {
                            lbtnVerSPECYES.CssClass += " disp-none";
                            lbtnVerSPECNO.CssClass = lbtnVerSPECNO.CssClass.Replace("disp-none", "");
                        }
                        else
                        {
                            lbtnVerSPECYES.CssClass = lbtnVerSPECYES.CssClass.Replace("disp-none", "");
                            lbtnVerSPECNO.CssClass += " disp-none";
                        }

                        string DocCount = drv["DocCount"].ToString();
                        LinkButton lbtnDeleteDoc = e.Row.FindControl("lbtnDeleteDoc") as LinkButton;

                        if (DocCount != "0")
                        {
                            lbtnDeleteDoc.CssClass += " disp-none";
                        }
                        else
                        {
                            lbtnDeleteDoc.CssClass = lbtnDeleteDoc.CssClass.Replace("disp-none", "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }


    }
}