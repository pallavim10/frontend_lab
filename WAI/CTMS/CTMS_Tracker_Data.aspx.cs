using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class CTMS_Tracker_Data : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                GetStructure(DSVISDAT);
                if (!this.IsPostBack)
                {
                    if (Session["PROJECTID"] != null)
                    {
                        Drp_Project.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                        bind_Tracker();
                        fill_Inv();
                        GetStructure(DSVISDAT);
                        GetRecords(DSVISDAT);
                    }
                    else
                    {
                        fill_Project();
                    }


                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void fill_Project()
        {
            try
            {

                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project.DataSource = ds.Tables[0];
                Drp_Project.DataValueField = "Project_ID";
                Drp_Project.DataTextField = "PROJNAME";
                Drp_Project.DataBind();
                Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
                if (Session["PROJECTID"] != null)
                {
                    Drp_Project.Items.FindByValue(Session["PROJECTID"].ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void fill_Inv()
        {
            try
            {

                DataSet ds = dal.GetSiteID(
                Action: "INVID",
                Project_Name: Drp_Project.SelectedItem.Text,
                User_ID: Session["User_ID"].ToString()
                );
                drp_INVID.DataSource = ds.Tables[0];
                drp_INVID.DataValueField = "INVNAME";
                drp_INVID.DataBind();
                drp_INVID.Items.Insert(0, new ListItem("--Select Inv ID--", "0"));
                //if (Session["CHECKLIST_INVID"] != null)
                //{
                //    if (drp_INVID.Items.FindByValue(Session["CHECKLIST_INVID"].ToString()) != null)
                //    {
                //        drp_INVID.Items.FindByValue(Session["CHECKLIST_INVID"].ToString()).Selected = true;
                //    }
                //}
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_Tracker()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.CTMS_Tracker(Action: "Get_TrackerList", ProjectID: Session["PROJECTID"].ToString());
                drpTracker.DataSource = ds.Tables[0];
                drpTracker.DataValueField = "TrackerId";
                drpTracker.DataTextField = "TrackerName";
                drpTracker.DataBind();
                drpTracker.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetStructure(GridView grd)
        {
            try
            {
                if (drpTracker.SelectedItem != null)
                {
                    if (drpTracker.SelectedValue != "0")
                    {
                        DataSet ds = dal.CTMS_Tracker(Action: "GET_Tracker_CheckList", ProjectID: Session["PROJECTID"].ToString(), TrackerId: drpTracker.SelectedValue);

                        grd.DataSource = ds;
                        grd.DataBind();

                        if (drpTracker.SelectedValue != "")
                        {
                            grd.Caption = drpTracker.SelectedItem.Text;
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
        private void GetRecords(GridView grd)
        {
            try
            {
                string  COLNAME, COLVAL;
                int rownum = 0;               

                //DataSet ds = dal.CTMS_Tracker(Action: "GET_Tracker_Data", ProjectID: Session["PROJECTID"].ToString(), TrackerId: drpTracker.SelectedValue,INVID:drp_INVID.SelectedValue);
                DataSet ds = dal.CTMS_Tracker(Action: "GET_Tracker_Data_View_BYINVID", ProjectID: Session["PROJECTID"].ToString(), TrackerName: drpTracker.SelectedItem.Text,INVID:drp_INVID.SelectedValue);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        grdData.DataSource = ds.Tables[0];
                        grdData.DataBind();

                        //for (rownum = 0; rownum < grd.Rows.Count; rownum++)
                        //{
                        //    COLNAME = ((Label)grd.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                        //    string CONTROLTYPE;
                        //    CONTROLTYPE = ((Label)grd.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                        //    string CHECKLIST_ID;
                        //    CHECKLIST_ID = ((Label)grd.Rows[rownum].FindControl("lblChecklistID")).Text;


                        //    if (CONTROLTYPE == "TEXTBOX")
                        //    {
                        //        COLVAL = ds.Tables[0].Rows[rownum]["DATA"].ToString();
                        //        ((TextBox)grd.Rows[rownum].FindControl(((Label)grd.Rows[rownum].FindControl("lblVARIABLENAME")).Text)).Text = COLVAL;
                        //    }
                        //    else if (CONTROLTYPE == "DROPDOWN")
                        //    {
                        //        COLVAL = ds.Tables[0].Rows[rownum]["DATA"].ToString();

                        //        DropDownList btnEdit = (DropDownList)grd.Rows[rownum].FindControl(COLNAME);

                        //        // ((DropDownList)grd.Rows[rownum].FindControl(COLNAME)).SelectedValue = COLVAL;
                        //        btnEdit.SelectedValue = COLVAL;
                        //    }
                        //    else if (CONTROLTYPE == "CHECKBOX")
                        //    {
                        //        COLVAL = ds.Tables[0].Rows[rownum]["DATA"].ToString();
                        //        if (COLVAL == "True")
                        //        {
                        //            ((CheckBox)grd.Rows[rownum].FindControl(((Label)grd.Rows[rownum].FindControl("lblVARIABLENAME")).Text)).Checked = true;
                        //        }
                        //        else
                        //        {
                        //            ((CheckBox)grd.Rows[rownum].FindControl(((Label)grd.Rows[rownum].FindControl("lblVARIABLENAME")).Text)).Checked = false;
                        //        }

                        //    }                         
                        //}
                    }
                    else
                    {
                        GetStructure(DSVISDAT);
                        grdData.DataSource = null;
                        grdData.DataBind();
                    }
                }
                else
                {
                    grdData.DataSource = null;
                    grdData.DataBind();
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
        protected void DSVISDAT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Label SubSection = (Label)DSVISDAT.FindControl("lblSubSection");
                    if (SubSection != null)
                    {
                        SubSection.Text = drpTracker.SelectedItem.Text;
                    }
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string DEFULTVAL = dr["DEFULTVAL"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string SUB_SECTION_YN = dr["SUB_SECTION_YN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();


                    string DROPDOWN_YN = dr["DROPDOWN_YN"].ToString();


                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Attributes.Add("class", "fontbold");
                    }
                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }
                    if (SUB_SECTION_YN == "True")
                    {
                        LinkButton lnkSubSection = (LinkButton)e.Row.FindControl("lnkSubSection");
                        lnkSubSection.Visible = true;
                    }
                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (MAXLEN != "" && MAXLEN != "0")
                        {
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);
                        }
                        if (READYN == "True")
                        {
                            btnEdit.ReadOnly = true;
                        }
                        if (DEFULTVAL != "")
                        {
                            btnEdit.Text = DEFULTVAL;
                        }
                        if (CLASS == "txtTime")
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = CLASS;
                        btnEdit.ToolTip = FIELDNAME;


                        DataSet ds;
                        ds = new DataSet();


                        if (DROPDOWN_YN == "False")
                        {
                            ds = dal.GetDropDownData(Action: "2", VariableName: VARIABLENAME);
                        }
                        else
                        {
                            ds = dal.GetDropDownData(Action: "1", VariableName: VARIABLENAME);
                        }
                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";

                        btnEdit.DataBind();
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        CheckBox btnEdit = (CheckBox)e.Row.FindControl("CHK_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = CLASS;
                        btnEdit.ToolTip = FIELDNAME;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }

        }
        protected void bntSaveComplete_Click(object sender, EventArgs e)
        {
            try
            {

                // GetStructure(DSVISDAT);
                InsertUpdatedata(DSVISDAT);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void InsertUpdatedata(GridView grd)
        {
            try
            {
                int rownum = 0;
                string strdata = "";
                string varname;

                for (rownum = 0; rownum < grd.Rows.Count; rownum++)
                {

                    varname = ((Label)grd.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                    string CONTROLTYPE;
                    CONTROLTYPE = ((Label)grd.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        strdata = ((TextBox)grd.Rows[rownum].FindControl(varname)).Text.Trim();
                    }
                    else if (CONTROLTYPE == "DROPDOWN")
                    {
                        strdata = ((DropDownList)grd.Rows[rownum].FindControl(varname)).SelectedValue.Trim();
                    }
                    else if (CONTROLTYPE == "CHECKBOX")
                    {
                        strdata = ((CheckBox)grd.Rows[rownum].FindControl(varname)).Checked.ToString();
                    }

                    DataSet ds = dal.CTMS_Tracker(
                    Action: "INSERT_Tracker_Data",
                    ProjectID: Drp_Project.SelectedItem.Value,
                    INVID: drp_INVID.SelectedItem.Value,
                    TrackerId: drpTracker.SelectedValue,
                    variablename: ((Label)grd.Rows[rownum].FindControl("lblVARIABLENAME")).Text,
                    controlType: ((Label)grd.Rows[rownum].FindControl("lblCONTROLTYPE")).Text,
                    FieldName: ((Label)grd.Rows[rownum].FindControl("lblFieldName")).Text,
                    DATA: strdata,
                    EnteredBy: Session["USER_ID"].ToString()
                    );
                }
                //Response.Write("<script> alert('Record Updated successfully.');window.location='CTMS_Tracker_Data.aspx'; </script>");
                Response.Write("<script> alert('Record Updated successfully.'); </script>");
                // Response.Write("<script> alert('Record Updated successfully.');window.location='CTMS_All_Checklist.aspx?SectionID='" + Drp_Section.SelectedValue + "'&SUBSECTIONID='" + Drp_SubSection.SelectedItem.Value +"'&Project_ID='" + Drp_Project.SelectedItem.Value + "'&INVID='" + drp_INVID.SelectedItem.Value + "'&MVID='" + Drp_MVID.SelectedItem.Value.Trim() + "; </script>");
                // Response.Redirect("CTMS_All_Checklist.aspx?SectionID=" + Drp_Section.SelectedValue + "&SUBSECTIONID=" + Drp_SubSection.SelectedItem.Value +"&Project_ID=" + Drp_Project.SelectedItem.Value + "&INVID=" + drp_INVID.SelectedItem.Value + "&MVID=" + Drp_MVID.SelectedItem.Value.Trim());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void Drp_Project_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Tracker();
                fill_Inv();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drp_INVID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRecords(DSVISDAT);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpTracker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetStructure(DSVISDAT);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Cells[0].Attributes.Add("class", "txt_center");
            }
        }


    }
}